<template>
  <div ref="whole_page">
    <vx-card v-show="showUpdateOptions">
      <vs-table stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" single @selected="selectedPost" pagination max-items="10" search :data="userLatestPhotos">
        <template slot="thead">
          <vs-th sort-key="postImage">Image</vs-th>
          <vs-th sort-key="caption">Caption</vs-th>
          <vs-th sort-key="amountFriendsSharedWith">Amount of friends shared with</vs-th>
        </template>
        <template  slot-scope="{data}">
          <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in data" >
            
            <b-avatar
            class="ml-3 mt-2"
            :src="tr.imageUrl"
            />

            <vs-td :data="data[indextr].caption">
              {{ tr.caption }}
            </vs-td>

            <vs-td :data="data[indextr].amountFriendsSharedWith">
              <vs-chip color="#101331">{{tr.leadStatusName}}</vs-chip>
            </vs-td>
          </vs-tr>
        </template>  
      </vs-table>
      <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
    </vx-card>
    <vx-card v-show="showEditCard" style="background-color:white">
      <div ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container cursor-pointer" @click="showUpload">
          <div class="text-center">
            <label style="color: black">Upload a new image (Click on the image to change to change it)</label>
            <div style="display: flex; flex-direction: column; align-items: center;">
              <img id="profileimg" style="border: 6px solid #838383;" v-if="updateImage" :src="updateImage" class="cursor-pointer mt-4" @click="showUpload">
            </div>
            <div v-if="updateImage" class="profile-overlay cursor-pointer">
              <div class="profile-overlay-text">Click to change image</div>
            </div>
            <div v-if="updateImage == null">
              <file-pond
              class="label"
              :server="uploadLocation"
              label-idle="Drop picture here or <span class='filepond--label-action' text-decoration-color: #aaa>Browse</span>"
              accepted-file-types="image/*"
              :allow-multiple="true"
              :instant-upload="true"
              @processfile="profilePicUploadComplete"
              @removefile="profilePicRemoved"
              />
            </div>
          </div>
        </div>
      </div>
      <vs-divider class="mt-5"></vs-divider>
      <div ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container">
          <div class="text-center">
            <label style="color: black">Update your caption</label>
            <vs-textarea counter="225" class="label" style="background-color: white; text-color: black !important; " placeholder="Write a caption..." v-model="updateCaption" ></vs-textarea>            
          </div>          
        </div>
      </div>
      <vs-divider class="mt-5"></vs-divider>
      <div ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container">
          <div class="text-center">
            <label v-show="!addFriendPopUp" style="color: black" class="mb-4">Update your friend access</label>
              <vs-table v-show="!addFriendPopUp" stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" single max-items="10" :data="getFriend">
                <template slot="thead">
                  <vs-th sort-key="profileImage">Profile Image</vs-th>
                  <vs-th sort-key="email">Email</vs-th>
                </template>
                <template  slot-scope="{data}">
                  <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in getFriend" >
                    
                    <b-avatar
                    class="ml-3 mt-2"
                    :src="tr.friendPfp"
                    />

                    <vs-td :data="data[indextr].email">
                      {{ tr.fullName }}
                    </vs-td>

                    <vs-td >
                      <vs-button color="danger" type="flat" icon-pack="feather" icon="icon-trash" @click="deleteFriendAccessClick(tr.friendUserId)"></vs-button>
                    </vs-td>
                  </vs-tr>
                </template>  
              </vs-table>
              <div v-show="!addFriendPopUp">
                <vs-button style="border-radius: 9px;" class="button mt-4" @click="getAllFriends">Add a friend to this post</vs-button>
              </div>
              <vx-card v-show="addFriendPopUp" class="mt-2 mb-5" style="background-color:white">
                <div class="vx-row">
                  <div class="vx-col w-full" style="display: flex; flex-direction: column; align-items: center;">
                    <div class="text-center">
                      <label class="mb-4" style="color: black">Add friend to a post</label>           
                    </div> 
                    <vs-table stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" single @selected="selectedAlbum" max-items="10" :data="allMyFriends">
                      <template slot="thead">
                        <vs-th sort-key="profileImage">Profile Image</vs-th>
                        <vs-th sort-key="fullName">Full Name</vs-th>
                      </template>
                      <template  slot-scope="{data}">
                        <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in allMyFriends" >
                          
                          <b-avatar
                            class=" mt-2"
                            :src="tr.imageUrl"
                            />

                          <vs-td :data="data[indextr].fullname">
                            {{ tr.fullName }}
                          </vs-td>

                          <vs-td >
                            <vs-button v-show="!addFriendToAlbumPopUp" color="success" type="flat" icon-pack="feather" icon="icon-check" @click="AddFriendToPhotoClick(tr.id)"></vs-button>
                            <vs-button v-show="addFriendToAlbumPopUp" color="success" type="flat" icon-pack="feather" icon="icon-check" @click="AddFriendToAlbumClick(tr.id)"></vs-button>
                          </vs-td>
                        </vs-tr>
                      </template>  
                    </vs-table>
                  </div>
                </div>
              </vx-card>
          </div>
        </div>
      </div>
      <vs-divider class="mt-5"></vs-divider>
      <div v-show="!AddPhotoPopUp" ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container">
          <div class="text-center">
            <!-- <vs-button style="border-radius: 9px;" class="button" @click="goHome">Add post to a ablum</vs-button> -->
          </div>
          <div class="vx-row mt-4" style="display: flex; justify-content: center;">
            <vs-button style="border-radius: 9px;" class="button" @click="AddPhotoButton">Add post to a album</vs-button>
          </div>
        </div>
      </div>
      <vx-card v-show="AddPhotoPopUp" class="mt-2 mb-5" style="background-color:white">
          <div class="vx-row">
            <div class="vx-col w-full" style="display: flex; flex-direction: column; align-items: center;">
              <div class="text-center">
                <label class="mb-4" style="color: black">Add post to a album</label>           
              </div> 
              <vs-table stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" single @selected="selectedAlbum" max-items="10" :data="allAlbums">
                <template slot="thead">
                  <vs-th sort-key="name">name</vs-th>
                </template>
                <template  slot-scope="{data}">
                  <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in allAlbums" >

                    <vs-td :data="data[indextr].name">
                      <img style="height: 53px; width: 50px" src="@/assets/images/pages/folder-icon-200.png" alt="latest-upload" class=" user-latest-image responsive">
                    </vs-td>

                    <vs-td :data="data[indextr].name">
                      {{ tr.name }}
                    </vs-td>

                    <vs-td >
                      <vs-button color="success" type="flat" icon-pack="feather" icon="icon-check" @click="AddImgToAlbumClick(tr.id)"></vs-button>
                    </vs-td>
                  </vs-tr>
                </template>  
              </vs-table>
            </div>
          </div>
        </vx-card>
      <div class="vx-row">
        <div class="vx-col w-1/2">
          <vs-button style="border-radius: 9px;" class="cancel-button" @click="cancelButton">Cancel</vs-button>
        </div>
        <div class="vx-col w-1/2" style="display: flex; align-items: flex-end; flex-direction: column;">
          <div class="vx-row">
              <div class="vx-col w-1/2">
              <vs-button style="border-radius: 9px;" class="delete-button" @click="deleteButton">Delete</vs-button>
            </div>
            <div class="vx-col w-1/2">
              <vs-button style="border-radius: 9px;" class="update-button" @click="updateButton">Update</vs-button>
            </div>
          </div>
        </div>
      </div>
    </vx-card>

    <vx-card v-show="addFriendToAlbumPopUp" class="mt-2 mb-5" style="background-color:white">
      <div class="vx-row">
        <div class="vx-col w-full" style="display: flex; flex-direction: column; align-items: center;">
          <div class="text-center">
            <label class="mb-4" style="color: black">Add friend to a album</label>           
          </div> 
          <vs-table stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" single  max-items="10" :data="allMyFriends">
            <template slot="thead">
              <vs-th sort-key="profileImage">Profile Image</vs-th>
              <vs-th sort-key="fullName">Full Name</vs-th>
            </template>
            <template  slot-scope="{data}">
              <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in allMyFriends" >
                
                <b-avatar
                  class=" mt-2"
                  :src="tr.imageUrl"
                  />

                <vs-td :data="data[indextr].fullname">
                  {{ tr.fullName }}
                </vs-td>

                <vs-td >
                  <vs-button v-show="addFriendToAlbumPopUp" color="success" type="flat" icon-pack="feather" icon="icon-check" @click="AddFriendAlbum(tr.id)"></vs-button>
                </vs-td>
              </vs-tr>
            </template>  
          </vs-table>
        </div>
      </div>
    </vx-card>

    <vx-card v-show="showGlobal" title="Latest Photos" class="mt-base" style="background-color: white;">
        <div>
          <vs-button v-show="!photosSharedWithMe" style="border-radius: 9px;" class="button mb-3" @click="updatePost">Update your post</vs-button>
          <vs-button v-show="!photosSharedWithMe" style="border-radius: 9px;" class="button ml-5" @click="AlbumClicked">Albums</vs-button>
          <vs-button v-show="photosSharedWithMe" style="border-radius: 9px;" class="button mb-3" @click="ViewAlbumClickedAsFriend">Albums Shared With me</vs-button>
        </div>
      <div class="vx-row w-full mb-4 ml-0">
        <div>
          <vs-checkbox v-model="photosSharedWithMe" color="warning">Show photos shared with me</vs-checkbox>
        </div>
      </div>
      <div class="vx-row w-full pt-2">
        <div class="vx-col w-1/2 sm:w-1/2 md:w-1/3 xl:1/4" :data="userDetail" v-for="(userDetail, index) in userLatestPhotos" :key="index">
          <vx-card class="trans rounded mb-5" style="background-color: #10163a;">
            <img style="height: 453px;" :src="userDetail.imageUrl" alt="latest-upload" class=" mb-4 user-latest-image responsive">
            <div class="vx-col w-full label-design" style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
              <label class="ml-2 mt-2" style="color: white;">{{userDetail.caption}}</label>
            </div>
          </vx-card>
        </div>
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3">
          <label v-show="userLatestPhotos.length == 0 && !photosSharedWithMe" class="ml-2 mb-2" style="color: black; display: flex; justify-content: center; font-size: x-large;">You have not yet created a Post</label>
          <label v-show="userLatestPhotos.length == 0 && photosSharedWithMe" class="ml-2 mb-2" style="color: black; display: flex; justify-content: center; font-size: x-large;">No posts has been shared with you</label>
        </div>
      </div>
      <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
    </vx-card>  

    <vx-card v-show="ShowAllAlbums" title="Latest Photos" class="mt-base" style="background-color: white;">
      <div class="vx-row w-full pt-1">
        <div class="vx-col w-1/2 sm:w-1/2 md:w-1/3 xl:1/4" :data="AlbumDetail" v-for="(AlbumDetail, index) in Albums" :key="index">
          <vx-card class="trans rounded mb-4 cursor-pointer" style="background-color: #10163a;">
            <div style="display: flex; flex-direction: row; justify-content: flex-end;">
              <vs-button color="danger" type="flat" icon-pack="feather" icon="icon-trash" @click="deleteAlbum(AlbumDetail.id)" ></vs-button>
            </div>
            <div class="vx-col w-full label-design" style="display: flex; flex-direction: row; justify-content: center; align-items: center;" @click="showAlbumPhotosClick(AlbumDetail.id)">
              <img style="height: 216px; width: initial;" src="@/assets/images/pages/folder-icon-200.png" alt="latest-upload" class=" mb-4 mt-4 user-latest-image responsive">
            </div>
            <div class="vx-col w-full label-design" style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
              <label class="ml-2 mt-2" style="color: white;">{{AlbumDetail.name}}</label>
            </div>
          </vx-card>
        </div>
      </div>
      <div class="vx-row">
        <div class="vx-col w-1/2">
          <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
        </div>
      </div>
    </vx-card>
  
    <vx-card v-show="showAlbumPhotos" title="Latest Photos" class="mt-base" style="background-color: white;">
      <div class="vx-col w-1/2" >
          <vs-button style="border-radius: 9px;" class="button mb-3" @click="addFriendToAlbumClickForPop">Add friend to album</vs-button>
        </div>
      <div class="vx-row w-full pt-2">
        <div class="vx-col w-1/2 sm:w-1/2 md:w-1/3 xl:1/4" :data="photo" v-for="(photo, index) in albumPhotos" :key="index">
          <vx-card class="trans rounded mb-5" style="background-color: #10163a;">
            <div style="display: flex; flex-direction: row; justify-content: flex-end;">
              <vs-button color="danger" type="flat" icon-pack="feather" icon="icon-trash" @click="deletePhoto(photo.id)" ></vs-button>
            </div>
            <div class="vx-col w-full label-design" style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
              <img style="height: 453px;" :src="photo.imageUrl" alt="latest-upload" class=" mb-4 user-latest-image responsive">
            </div>
            <div class="vx-col w-full label-design" style="display: flex; flex-direction: row; justify-content: center; align-items: center;">
              <label class="ml-2 mt-2" style="color: white;">{{photo.caption}}</label>
            </div>
          </vx-card>
        </div>
      </div>
      <div class="vx-row">
        <div class="vx-col w-1/2">
          <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
        </div>
      </div>
    </vx-card>

      <b-modal
        id="move-popup"
        title="Create a new album"
        v-model="showCreateAlbum"
        scrollable
        no-close-on-backdrop
        style="margin-top: 175px;"
        cancel-title="Cancel"
        @cancel="initValues"
        ok-title="Create"
        @ok="createAlbum"  
        cancel-variant="CancelBTN"
        modal-class="modal-success"
        centered
      >
        <b-card-text style="color:#000000">
          <div class="vx-row">
            <div class="vx-col w-full">
              <h6 class="mb-2" style="color: black">Please enter the new album name</h6>
              <vs-input
                class="w-full"
                v-validate="'required'"
                type="text" 
                name="name"
                icon-no-border
                icon="icon icon-layers"
                icon-pack="feather"
                placeholder="Album name"
                v-model="album.name"
                />
              <span class="text-danger text-sm">{{ errors.first('name') }}</span>
            </div>
          </div>
        </b-card-text>
      </b-modal>

      <b-modal
        id="move-popup"
        title="Album"
        v-model="ShowAlbumPopUp"
        scrollable
        hide-footer
        no-close-on-backdrop
        style="margin-top: 175px;"
        cancel-title="Cancel"
        @cancel="initValues"

        cancel-variant="CancelBTN"
        modal-class="modal-success"
        centered
      >
      <b-card-text style="color:#000000">
          <div class="vx-row">
            <div class="vx-col w-1/2" style="display: flex; flex-direction: row; align-content: center; justify-content: center;">
              <vs-button style="border-radius: 9px;" class="button mb-3" @click="ViewAlbumClicked">View Albums</vs-button>
            </div>
            <div class="vx-col w-1/2" >
              <vs-button style="border-radius: 9px;" class="button mb-3" @click="CreateAlbumClicked">Create a New Album</vs-button>
            </div>
          </div>
        </b-card-text>
      </b-modal>
  </div>
</template>

<script>

import Gallery from "vuejs-image-gallery";

import vSelect from 'vue-select'

import { WebCam } from 'vue-web-cam'

import vueFilePond from 'vue-filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginImagePreview from 'filepond-plugin-image-preview';
const FilePond = vueFilePond(FilePondPluginFileValidateType, FilePondPluginImagePreview);


import VueSimpleSuggest from 'vue-simple-suggest';
import 'vue-simple-suggest/dist/styles.css'
import VxCard from '../../components/vx-card/VxCard.vue';

export default {
  components: {
    vSelect,
    WebCam,
    'v-select': vSelect,
    FilePond,
    VueSimpleSuggest,
    Gallery,
    VxCard
  },
  data() {
    return {
      getFriend:[],
      selectedAlbumId: 0, 
      addFriendToAlbumPopUp: false,
      allMyFriends:[],
      addFriendPopUp: false,
      allAlbums:[],
      Albums:[],
      AddPhotoPopUp: false,
      albumId: 0,
      photoId: 0,
      showAlbumPhotos: false,
      ShowAllAlbums: false,
      ShowAlbumPopUp: false,
      totaleAmountUsers: 0,
      photosSharedWithMe: false,
      album:{
        name: "",
        userId: 0,
      },
      showCreateAlbum: false,
      imgId: 0,
      updateData:{
        imageUrl: null,
        caption: "",
      },
      updateCaption: "",
      updateImage: "",
      showGlobal: true,
      showEditCard: false,
      showUpdateOptions: false,
      logedinUserId: 0,
      userLatestPhotos: [],
      createNewPost: true,
      formData: {
        id: 0,
        userFirstName: "",
        userLastName: "",
        mobile: "",
        email: "",
        uploadedFileName: null,
        password: "",
        passwordConfirm: "",
        caption: ""
      },
      changePassword: false,
      searchResults: [],
      img: null,
      camera: null,
      deviceId: null,
      devices: [],
      frontCam: true,
      WebCam: false,
      currentDocumentToUploadType: null,
      currentDocumentId: 0
    }
  },
  metaInfo () {
    return {
    }
  },
  computed:{
    uploadLocation: {
      get() {
        return this.$buildUrl("tempupload");
      }
    },
    device: function () {
      return find(this.devices, n => n.deviceId == this.deviceId);
    }
  },
  watch: {
    photosSharedWithMe: function(val) {
      debugger
      this.getUserInfo(val);
    },
    camera: function (id) {
      this.deviceId = id;
    },
    devices: function () {
      if (typeof window.orientation === 'undefined') {
        // Once we have a list select the first one
        let first = this.devices[0];
        if (first) {
          this.camera = first.deviceId;
          this.deviceId = first.deviceId;
        }
      } else {
        this.frontCam = false;
      }
    },
  },
  methods: {
    goHome(){
      var self = this;
      self.$router.push('/pages/home')
    },
    cancelButton(){
      var self = this;
      self.showUpdateOptions = false;
      self.showGlobal = true;
      self.showEditCard = false;
      this.getUserInfo(false)
    },
    getUserInfo(val) {
      var self = this;
      debugger
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.showLoadingOnElement("whole_page", 1, "material");
      var onSuccess = function (response) {
        debugger
        self.userLatestPhotos = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      if(val){
        self.$ajaxGetAnon(self,"PostLog/GetPostssharedWithMe?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
      }
      else{
        self.$ajaxGetAnon(self,"PostLog/GetUserLatestPosts?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
      }
      
    },
    // getAmountOfFriends() {
    //   var self = this;
    //   debugger
    //   self.logedinUserId = parseInt(localStorage.getItem("userId"));
    //   self.showLoadingOnElement("whole_page", 1, "material");
    //   var onSuccess = function (response) {
    //     debugger
    //     self.totaleAmountUsers = response.data;
    //   };
    //   var onFinally = function () {
    //     self.$vs.loading.close(self.$refs.whole_page.$el);
    //   };
    //     self.$ajaxGetAnon(self,"PostLog/GetUserLatestPosts?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
      
    // },
    showLoadingOnElement(element, scale, type){
      var self = this;
      self.$vs.loading({
        type: type,
        container: self.$refs[element].$el,
        scale: scale
      });
    },
    scrollToNavbar() {
      var scrollOptions = {
        easing: 'ease-in',
        offset: -500,
        force: true,
        cancelable: true,
        x: false,
        y: true
      }
      var cancelScroll = this.$scrollTo(document.getElementById("navbarGlobal"), 500, scrollOptions);
    },
    updatePost(){
      var self = this;
      debugger
      self.showUpdateOptions = true;
      self.showGlobal = false;
    },
    selectedPost(userLatestPhotos){
      var self = this;
      debugger
      self.imgId = userLatestPhotos.id;
      self.updateImage = userLatestPhotos.imageUrl;
      self.updateCaption = userLatestPhotos.caption;
      self.showEditCard = true;
      self.showUpdateOptions = false;
      this.getFriends();
    },
    updateButton(){
      debugger
      var self = this;
      // self.showLoadingOnElement("postButton", 0.45, "material");
      self.updateData.id = self.imgId;
      self.updateData.imageUrl = self.updateImage;
      self.updateData.caption = self.updateCaption;

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Post Updated",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.showUpdateOptions = false;
        self.showGlobal = true;
        self.showEditCard = false;
        self.getUserInfo(false);
      };
      debugger
      self.$ajaxPostAnon(self, "PostLog/UpdatePost", self.updateData, onSuccess, onFinally);
    },
    deleteButton(){
      debugger
      var self = this;
      // self.showLoadingOnElement("postButton", 0.45, "material");
      self.updateData.id = self.imgId;

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Post Updated",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.showUpdateOptions = false;
        self.showGlobal = true;
        self.showEditCard = false;
        self.getUserInfo(false);
      };
      self.$ajaxPostAnon(self, "PostLog/DeletePost?postId=" + self.updateData.id, onSuccess, onFinally);
    },






    profilePicUploadComplete(error, file) {
      var response = JSON.parse(file.serverId);
      if(response.url)
      debugger
        this.updateImage = response.url;
    },
    profilePicRemoved(error, file) {
      this.UpdateData.uploadedFileName = "";
    },
    showUpload() {
      debugger
      this.updateImage = null;
      this.WebCam = true;
    },
    async onCapture () {
      this.img = await this.$refs.webcam.capture();

      var block = this.img.split(";");
      var contentType = block[0].split(":")[1];
      if(contentType != ","){
        var realData = block[1].split(",")[1];
        var blob = await this.b64toBlob(realData, contentType);
        this.uploadSnapshot(blob);
      }
    },
    b64toBlob(b64Data, contentType, sliceSize) {
      contentType = contentType || '';
      sliceSize = sliceSize || 512;

      var byteCharacters = window.atob(b64Data);
      var byteArrays = [];

      for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
          byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
      }

      var blob = new Blob(byteArrays, {type: contentType});
      return blob;
    },
    uploadSnapshot(blob) {
      var self = this;
      var formData = new FormData();
      formData.append('image', blob, "agent_" + self.formData.id + ".jpg");

      var onSuccess = response => {
        self.formData.uploadedFileName = response.data.url;
        self.WebCam = false;
      };
      self.$ajaxPostAnon(self, "tempupload", formData, onSuccess);
    },
    showUploadDialog(typeId, fileId) {
      var self = this;
      self.currentDocumentToUploadType = typeId;
      self.currentDocumentId = fileId || 0;
      self.$refs.documentpond.browse();
    },
    beforeUpload(file) {
      this.showLoadingOnElement("documentsTable", 1, "material");
    },
    startUpload(file) {
      
    },
    uploadProgress(file, progress) {
      
    },
    docTempUploadComplete(error, file) {
      var self = this;
      var response = JSON.parse(file.serverId);
      if(response.url) {
        self.$vs.loading.close(self.$refs.documentsTable.$el);
        setTimeout(function() {
          self.uploadDocument(response.url);
        }, 100);
      }
    },
    showLoadingOnElement(element, scale, type){
      var self = this;
      self.$vs.loading({
        type: type,
        container: self.$refs[element].$el,
        scale: scale
      });
    },
    CreateAlbumClicked(){
      var self = this;
      self.showCreateAlbum = true;
    },
    AlbumClicked(){
      var self = this;
      self.ShowAlbumPopUp = true;
    },
    createAlbum(){
      debugger
      var self = this;
      // self.showLoadingOnElement("postButton", 0.45, "material");
      self.album.userId = parseInt(localStorage.getItem("userId"));
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "New Album Created",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
      };
      self.$ajaxPostAnon(self, "Album/CreateAlbum", self.album, onSuccess, onFinally);
    },
    ViewAlbumClicked(){
      var self = this;
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        debugger
        self.ShowAllAlbums = true;
        self.showGlobal = false;
        self.ShowAlbumPopUp = false;
        debugger
        self.Albums = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
        self.$ajaxGetAnon(self,"Album/GetAlbums?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    ViewAlbumClickedAsFriend(){
      var self = this;
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        debugger
        self.ShowAllAlbums = true;
        self.showGlobal = false;
        self.ShowAlbumPopUp = false;
        debugger
        self.Albums = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
        self.$ajaxGetAnon(self,"Album/GetAlbumsAsAFriend?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    ViewAllAlbumClicked(){
      var self = this;
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        self.allAlbums = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
        self.$ajaxGetAnon(self,"Album/GetAlbums?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    showAlbumPhotosClick(val){
      var self = this;
      debugger
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.albumId = parseInt(val);
      self.selectedAlbumId = self.albumId;
      // self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        debugger
        self.ShowAllAlbums = false;
        self.showGlobal = false;
        self.ShowAlbumPopUp = false;
        self.showAlbumPhotos = true;
        debugger
        self.albumPhotos = response.data;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"AlbumPhotos/GetPhotos?AlbumId=" + self.albumId, onSuccess, onFinally);
    },
    getFriends(){
      var self = this;
      debugger
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      var postId = self.imgId;
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        debugger
        self.getFriend = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
        self.$ajaxGetAnon(self,"ViewPost/GetUser?logedinUserId=" + self.logedinUserId + "&postId=" + postId, onSuccess, onFinally);
    },
    deleteAlbum(val){
      debugger
      var self = this;
      self.albumId = parseInt(val);
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "The album was deleted",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push('/pages/home')
      };
      self.$ajaxPostAnon(self, "Album/DeleteAlbum?albumId=" + self.albumId, onSuccess,onFinally);
    },
    deletePhoto(val){
      debugger
      var self = this;
      self.photoId = parseInt(val);
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Photo has been removed from album",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push('/pages/home')
      };
      self.$ajaxPostAnon(self, "AlbumPhotos/DeletePhoto?photoId=" + self.photoId + "&albumId=" + self.albumId, onSuccess, onFinally);
    },
    deleteFriendAccessClick(val){
      debugger
      var self = this;
      var friendUserId = parseInt(val);
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Photo has been removed from album",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push('/pages/home')
      };
      debugger
      self.$ajaxPostAnon(self, "ViewPost/DeleteFriendAccess?photoId=" + self.imgId + "&friendUserId=" + friendUserId, onSuccess, onFinally);
    },
    AddPhotoButton(){
      var self = this;
      self.AddPhotoPopUp = true;
    },
    AddImgToAlbumClick(val){
      debugger
      var self = this;
      self.albumId = parseInt(val);
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Photo has been added to album",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
        self.AddPhotoPopUp = false;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
      };
      self.$ajaxPostAnon(self, "AlbumPhotos/AddPhoto?photoId=" + self.imgId + "&albumId=" + self.albumId, onSuccess, onFinally);
    },
    AddFriendToPhotoClick(val){
      debugger
      var self = this;
      self.friendId = parseInt(val);

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "User has been added to photo",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.addFriendPopUp = true;
        self.$router.push('/pages/homeF');
      };
      self.$ajaxPostAnon(self, "ViewPost/AddFriendToPhoto?photoId=" + self.imgId + "&friendId=" + self.friendId, onSuccess, onFinally);
    },
    AddFriendAlbum(val){
      debugger
      var self = this;
      self.friendId = parseInt(val);

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "User has been added to photo",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push("/pages/home")
      };
      self.$ajaxPostAnon(self, "AlbumUserAccess/AddFriendToAlbum?albumId=" + self.selectedAlbumId + "&friendId=" + self.friendId, onSuccess, onFinally);
    },
    getAllFriends(){
      var self = this;
      self.addFriendPopUp = true;
      debugger
      self.logedinUserId = parseInt(localStorage.getItem("userId"));
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        debugger
        self.allMyFriends = response.data;
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
        self.$ajaxGetAnon(self,"Friends/GetMyFriend?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    addFriendToAlbumClickForPop(){
      var self = this;
      debugger
      self.addFriendToAlbumPopUp = true;
      self.getAllFriends();
      self.addFriendPopUp = false;
      self.showAlbumPhotos = false;
    },
  },
  mounted() {
    // this.scrollToNavbar();
    debugger
    // self.logedinUserId = localStorage.getItem("userId");
    this.getUserInfo(false)
    this.ViewAllAlbumClicked();
    
  },
}
</script>
<style lang="scss">
  #profileimg {
    // border-radius: 50%;
    padding:2px;
    border:1px solid #838383;
    background-color:rgb(255, 255, 255);
    // /* max-width: 100px; */
    object-fit: cover !important;
    background-color: white !important;
    width: 450px !important;
    height: 390px !important;
  }
  #profilerow {
    align-items: center;
  }
  .profile-overlay-text {
    border-radius: 50%;
    color: rgb(41, 41, 41);
    font-size: 20px;
    position: absolute;
    top: 50%;
    left: 50%;
    -webkit-transform: translate(-50%, -50%);
    -ms-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);
    text-align: center;
    text-shadow: 2px 2px 5px rgb(255, 255, 255);
  }
  .profile-container:hover .profile-overlay {
    opacity: 1;
    border-radius: 50%;
  }
  .profile-container {
    position: relative;
    width: 50%;
    border-radius: 50%;
  }
  .profile-overlay {
    border-radius: 50%;
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    height: 100%;
    width: 100%;
    opacity: 0;
    transition: .5s ease;
  }
  .theme-dark input {
    background: #ffffff !important;
    color: black !important;

      .theme-dark .vs-con-textarea .vs-textarea {
        color: black !important;
      }
  }
  .theme-dark .vx-card .vx-card__title h4{
    // background: #ffffff !important;
    color: darkgrey !important;
  }
  .button{
    background: linear-gradient(to left, rgb(46, 89, 132) 0%, rgb(51, 204, 255) 100%) !important;
    color: white !important;
  }
  .update-button{
    background: linear-gradient(to left, rgb(46, 132, 60) 0%, rgb(58, 255, 51) 100%) !important;
    color: white !important;
  }
  .delete-button{
    background: linear-gradient(to left, rgb(132, 46, 46) 0%, rgb(255, 51, 51) 100%) !important;
    color: white !important;
  }
  .cancel-button{
    background: gray !important;
    color: white !important;
  }
  .label-design{
    border-top-style: solid;
    border-bottom-style: solid;
    border-left-style: solid;
    border-right-style: solid;
  }
  .trans{
    &:hover{
      transform: scale(1.02);
    }
  }
  .addButton{
    // background-image: linear-gradient(315deg, #08e1ae 0%, #98de5b 74%);
    background: linear-gradient(315deg, #08e1ae 0%, #25b845 74%) !important;
  }
  // .theme-dark .vx-card{
  //   color: black !important;
  //   background-color: #003152 !important;
  // }
  // .theme-dark .vs-con-table .vs-con-tbody {
  //   background: #ffffff;
  //   border: 0.5px solid #003152 !important;
  //   }

  // .theme-dark .vs-con-table .vs-con-tbody .vs-table--tbody-table tr {
  //   border: 1px solid whitesmoke !important;
  // }

</style>


