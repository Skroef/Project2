<template>
  <div ref="whole_page">

    <b-modal
        id="move-popup"
        v-model="showOptionsPopUp"
        scrollable
        no-close-on-backdrop
        style="margin-top: 175px;"
        ok-title="Cancel"
        ok-only
        @ok="initValues"  
        ok-variant="secondary"
        modal-class="modal-success"
        centered
      >
        <b-card-text style="color:#000000">
          <div class="vx-row">
            <div class="vx-col w-full">
              <h4 class="mb-2" style="color: black; display: flex; justify-content: center;">Choose a Option to share with.</h4>
                <div class="vx-row ">
                  <div class="vx-col w-full" style="display: flex; justify-content: center">
                    <vs-button
                      style="border-radius: 9px;"
                      icon-pack="feather"
                      icon="icon-users"
                      color="grey"
                      type="filled"
                      ref="postButton"
                      class="mt-2 mb-4 vs-con-loading__container cursor-pointer button"
                      @click="sareAll"
                      >Share to <strong>All</strong> friends</vs-button>
                  </div>
                </div>
                <div class="vx-col w-full" style="display: flex; justify-content: center">
                  <vs-button
                    style="border-radius: 9px;"
                    icon-pack="feather"
                    icon="icon-user"
                    color="grey"
                    type="filled"
                    ref="postButton"
                    class="mt-2 mb-4 vs-con-loading__container cursor-pointer button"
                    @click="shareSpecific"
                    >Share to <strong>Specific</strong> friends</vs-button>
                      
                </div>
            </div>
          </div>
        </b-card-text>
      </b-modal>


    <!-- Share to Specific Card -->
    <vx-card v-show="shareToSpecific" style="background-color: white;">
      <div ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container">
          <div class="text-center">
            <vx-card class="mb-5">
              <v-select
                class="mb-5" 
                id="bodytypedropdown" 
                style="margin-top: 10px; color-black" 
                placeholder="Select A Friend"
                @input="selected"
                v-model="selectedFriendId"
                :options="options"
                :dir="$vs.rtl ? 'rtl' : 'ltr'">
              </v-select>
              <!-- <vs-button style="border-radius: 9px;" class="button" @click="selectAFriend(selectedFriendId)">Confirm selected friend</vs-button> -->
              <!-- <vs-table stripe ref="leadsTable" v-model="selected" class="vs-con-loading__container" pagination max-items="10" :data="setFriends">
                <template slot="thead">
                  <vs-th sort-key="email">Selected User email</vs-th>
                </template>
                <template  slot-scope="{data}">
                  <vs-tr :data="tr" :key="indextr" v-for="(tr, indextr) in setFriends" >

                    <vs-td :data="data[indextr].email">
                      {{ tr.label }}
                    </vs-td>

                  </vs-tr>
                </template>  
              </vs-table> -->
            </vx-card>
            <img id="profileimg" v-if="formData.uploadedFileName" :src="formData.uploadedFileName" class="cursor-pointer mt-4" @click="showUpload">
            <div v-if="formData.uploadedFileName" class="profile-overlay cursor-pointer">
              <div class="profile-overlay-text">Click to change image</div>
            </div>
            
            <!-- <vx-card class="mb-5"> -->
            <div v-if="formData.uploadedFileName == null">
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
              <!-- </vx-card> -->
          </div>
        </div>
      </div>
      <div class="vx-row">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3">
          <div class="vx-row pt-4 pb-3">
            <div class="vx-col  w-full mb-2">
              <vs-textarea counter="225" class="label" style="background-color: white; text-color: black !important; " placeholder="Write a caption..." v-model="formData.caption" ></vs-textarea>
            </div>
          </div>
        </div>
        
      </div>
      <div class="vx-row w-full" style="margin-left: auto;">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3" style="display: flex; justify-content: center;">
          <vs-button
          style="border-radius: 9px;"
          icon-pack="feather"
          icon="icon-send"
          color="success"
          type="filled"
          ref="postButton"
          class="mt-2 mb-4 vs-con-loading__container cursor-pointer"
          @click="selectAFriendPost"
          >Post</vs-button>
        </div>
      </div>
      <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
      
    </vx-card>




<!-- Share to all Card -->
    <vx-card v-show="shareToAll" style="background-color: white;">
      <div ref="profilerow" class="vx-row vs-con-loading__container">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3  mt-4 profile-container cursor-pointer" @click="showUpload">
          <div class="text-center">
            <img id="profileimg" v-if="formData.uploadedFileName" :src="formData.uploadedFileName" class="cursor-pointer mt-4">
            <div v-if="formData.uploadedFileName" class="profile-overlay cursor-pointer">
              <div class="profile-overlay-text">Click to change image</div>
            </div>
            <!-- <div class="centerx">
              <vs-upload automatic action="uploadLocation" :server="uploadLocation" @on-success="successUpload" />
            </div> -->
            
            
            <div v-if="formData.uploadedFileName == null">
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
      <div class="vx-row">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3">
          <div class="vx-row pt-4 pb-3">
            <div class="vx-col  w-full mb-2">
              <vs-textarea counter="225" class="label" style="background-color: white; text-color: black !important; " placeholder="Write a caption..." v-model="formData.caption" ></vs-textarea>
            </div>
          </div>
        </div>
        
      </div>
      <div class="vx-row w-full" style="margin-left: auto;">
        <div class="vx-col w-1/3"></div>
        <div class="vx-col w-1/3" style="display: flex; justify-content: center;">
          <vs-button
          style="border-radius: 9px;"
          icon-pack="feather"
          icon="icon-send"
          color="success"
          type="filled"
          ref="postButton"
          class="mt-2 mb-4 vs-con-loading__container cursor-pointer"
          @click="createPost"
          >Post</vs-button>
        </div>
      </div>
      <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
    </vx-card>
  </div>
</template>

<script>
import vSelect from 'vue-select'

import { WebCam } from 'vue-web-cam'

import vueFilePond from 'vue-filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginImagePreview from 'filepond-plugin-image-preview';
const FilePond = vueFilePond(FilePondPluginFileValidateType, FilePondPluginImagePreview);

import VueSimpleSuggest from 'vue-simple-suggest';
import 'vue-simple-suggest/dist/styles.css'

export default {
  components: {
    vSelect,
    WebCam,
    'v-select': vSelect,
    FilePond,
    VueSimpleSuggest
  },
  data() {
    return {
      options: [],
      setFriends:[],  
      selectedFriendId: 0, 
      getFriends: [],
      showOptionsPopUp: false,
      shareToSpecific: false,
      shareToAll: false,
      uploadedDocuments: [],
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
        debugger
        return this.$buildUrl("tempupload");
      }
    },
    device: function () {
      return find(this.devices, n => n.deviceId == this.deviceId);
    }
  },
  watch: {
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
    successUpload(){
      debugger
      // this.$vs.notify({color:'success',title:'Upload Success',text:'Lorem ipsum dolor sit amet, consectetur'})

    },
    createPost(){
      debugger
      var self = this;
      // self.showLoadingOnElement("postButton", 0.45, "material");
      var routeData = null;
      var pack = {
        userId: parseInt(localStorage.getItem("userId")),
        imageUrl: self.formData.uploadedFileName,
        caption: self.formData.caption,
      };

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Post Created",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push('/pages/viewPhotos');
      };
      self.$ajaxPostAnon(self, "PostLog/CreatePost", pack, onSuccess, onFinally);

    },
    goHome(){
      var self = this;
      self.$router.push('/pages/home')
    },
    initValues(){
      var self = this; 
      self.$router.push('/pages/home')
    },
    // mapsInit () {
    //   this.service = new window.google.maps.places.AutocompleteService()
    // },
    onCameras (cameras) {
      this.devices = cameras;
      //this.onSuccess(); <-- for testing only
    },
    profilePicUploadComplete(error, file) {
      var response = JSON.parse(file.serverId);
      if(response.url)
        this.formData.uploadedFileName = response.url;
    },
    profilePicRemoved(error, file) {
      this.formData.uploadedFileName = "";
    },
    showUpload() {
      this.formData.uploadedFileName = null;
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
    onSuccess () {
      this.$vs.dialog({
        color: 'success',
        title: 'Camera Accessed!',
        text: 'Great! Now you can start!',
        acceptText: "Thanks",
      })
    },
    // getUserInfo() {
    //   var self = this;
    //   self.showLoadingOnElement("whole_page", 1, "material");

    //   var onSuccess = function (response) {
    //     self.formData.id = response.data.id;

    //     // self.getDocuments();
    //   };
    //   var onFinally = function () {
    //     self.$vs.loading.close(self.$refs.whole_page.$el);
    //   };
    //   var email = "Hennie@mweb.co.za"
    //   self.$ajaxGetAnon(self,"user/GetCurrentUserInfo?email=" + email, onSuccess, onFinally);
    // },
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
    searchForFriend(){},
    sareAll(){
      var self = this;
      self.showOptionsPopUp = false;
      self.shareToAll = true;
    },
    shareSpecific(){
      var self = this;
      self.shareToSpecific = true;
      self.showOptionsPopUp = false;
    },
    getFriendFromAPI() {
      var self = this;
      self.logedinUserId = localStorage.getItem("userId");
      debugger
      // self.showLoadingOnElement("whole_page", 1, "material");
      var onSuccess = function (response) {
        debugger
        self.getFriends = response.data;
        self.options = response.data.map((item) => {return {label: item.fullName, value: item.id}});
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"Friends/GetSpecificFriend?logedinUserId=" + self.logedinUserId , onSuccess, onFinally);
    },

    selected(){
      var self = this;
      debugger
      self.setFriends.push(self.selectedFriendId.value);
    },
    selectAFriendPost(){
      debugger
      var self = this;
      // self.showLoadingOnElement("postButton", 0.45, "material");
      var specificPostDetails = {
        ParentUserId: parseInt(localStorage.getItem("userId")),
        userId: self.setFriends,
        imageUrl: self.formData.uploadedFileName,
        caption: self.formData.caption,
      };
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Post Created",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.$router.push('/pages/viewPhotos');
      };
      debugger
      self.$ajaxPostAnon(self, "ViewPost/AddSpesificUsersToPost", specificPostDetails, onSuccess, onFinally);

    },
  },
  mounted() {
    var self = this;
    self.showOptionsPopUp = true;
    debugger
    self.getFriendFromAPI();
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
  .button{
    background: linear-gradient(to left, rgb(46, 89, 132) 0%, rgb(51, 204, 255) 100%) !important;
    color: white !important;
  }
  .modal-width{
    width: 840px !important;
  }
  .theme-dark input {
  background: #ffffff !important;
  color: black !important;

    .theme-dark .vs-con-textarea .vs-textarea {
      color: black !important;
    }
  
  }
</style>


