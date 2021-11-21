<template>
  <div id="profile-page" >
    <!-- PROFILE HEADER -->
    <div class="profile-header">
      <!-- <div class="relative">
        <div class="cover-container rounded-t-lg">
          <img :src="user_info.cover_img" alt="user-profile-cover" class="responsive block">
        </div>
        <div class="profile-img-container pointer-events-none">
          <div>
            <vs-avatar class="user-profile-img" :src="user_info.profile_img" size="85px" />
          </div>
        </div>
      </div> -->
      <div style="border-radius: 12px;" class="flex items-center justify-end flex-wrap profile-header-nav p-6">
        <div :class="isNavOpen ? 'block': 'hidden'" class="w-full flex-grow sm:flex sm:items-center sm:w-auto">
          <div class="text-sm sm:flex-grow">
            <ul class="sm:flex justify-around mt-8 w-full md:mt-0 md:ml-auto md:w-2/4">
              <li class="pt-2 sm:p-0"><router-link to="/pages/viewPhotos" style="color: white">My Photos</router-link></li>
              <li class="pt-2 sm:p-0"><router-link to="/pages/newPost" style="color: white">Create New Post</router-link></li>
              <li class="pt-2 sm:p-0"><router-link to="/pages/friendList" style="color: white">My Friends</router-link></li>
            </ul>
          </div>
        </div>
      </div>
      <vs-input icon-no-border placeholder="Search Topic or Keyword" v-model="knowledgeBaseSearchQuery" icon-pack="feather" icon="icon-search" size="large" class="mt-6" />
    </div>
    <!-- COL AREA -->
    <div class="vx-row">
        <!-- COL 1 -->
        <div class="vx-col w-full lg:w-1/4">
          <vx-card v-show="getFriendRequest.length != 0" title="Follower Requests" class="mt-base">
            <!-- FRIENDS LIST -->
            <ul class="friend-suggesions-list">
                <li class="friend-suggestion flex items-center mb-4" v-for="(friend, index) in getFriendRequest" :key="index">
                    <div class="mr-3"><vs-avatar class="m-0" :src="friend.imageUrl" size="35px" /></div>
                    <div class="leading-tight">
                        <p class="font-medium">{{ friend.name }}</p>
                    </div>
                    <div class="ml-auto cursor-pointer">
                      <vs-button style="color: white; border-color: white" radius type="border" icon-pack="feather" icon="icon-user-plus" @click="acceptFriendRequest(friend)"/>
                    </div>
                </li>
            </ul>
          </vx-card>
        </div>
        <div class="vx-col w-full lg:w-1/2">
          <vx-card class="mt-base" style="backgroundColor: #2E5984; color: white" v-for="post in filteredKB" :key="post.postLogId">
            <div>
              <!-- POST HEADER -->
              <div class="post-header flex justify-between mb-4">
                <div class="flex items-center">
                  <div>
                    <vs-avatar class="m-0" :src="post.friendPfp" size="45px"></vs-avatar>
                  </div>
                  <div class="ml-4">
                    <h6>{{ post.fullName }}</h6>
                    <small>{{ post.createdOn.replace("T", ' ') }}</small>
                  </div>
                </div>
              </div>
              <!-- POST CONTENT -->
              <div class="post-content">
                <p>{{ post.caption }}</p>
              </div>
              <div class="post-media-container mb-6 mt-4">
                <img  style="height: 568px;" class="responsive rounded" :src="post.imageUrl" alt="user-upload" >
                <div class="ml-auto cursor-pointer mt-4">
                  <vs-button style="color: red; background-color: white !important; border-radius: 7px;" radius  icon-pack="feather" icon="icon-download" @click="downloadImage(post.imageUrl)"/>
                </div>
              </div>
              <!-- POST ACTION DATA -->
              <div>
                <div class="comments-container mt-4">
                  <ul class="user-comments-list">
                    <li v-for="comments in post.postComments" :key="comments.id" class="commented-user flex items-center mb-4">
                      <div class="mr-3"><vs-avatar class="m-0" :src="comments.pfpImageUrl" size="30px" /></div>
                      <div class="leading-tight">
                        <p class="font-medium">{{ comments.commentorFullName }}</p>
                        <span class="text-xs">{{ comments.text }}</span>
                      </div>
                    </li>
                  </ul>
                </div>
                <div class="post-comment">
                  <vs-textarea label="Add Comment" class="mb-2" v-model="postComments.text" />
                  <vs-button size="small" @click="commentBox(post.postLogId)">Post Comment</vs-button>
                </div>
              </div>
            </div>
          </vx-card>
        </div>
      <div class="vx-col w-full lg:w-1/4">
        <!-- LATEST PHOTOS -->
        <vx-card v-show="userLatestPhotos.length != 0" title="Latest Photos Posted" class="mt-base scroll" style="height: 320px;" >
          <div class="vx-row pt-2">
            <div class="vx-col w-1/2 sm:w-1/2 md:w-1/3 xl:1/4" :data="userDetail" v-for="(userDetail, index) in userLatestPhotos" :key="index">
              <img style="height: 100px;" :src="userDetail.imageUrl" alt="latest-upload" class="rounded mb-4 user-latest-image responsive">
            </div>
          </div>
        </vx-card>
      </div>
    </div>
    <!-- <div class="vx-row">
      <div class="vx-col w-full">
        <div class="flex justify-center mt-base">
          <vs-button id="button-load-more-posts" class="vs-con-loading__container" @click="loadContent">Load More</vs-button>
        </div>
      </div>
    </div> -->
  </div>
</template>

<script>
import { videoPlayer } from 'vue-video-player'
import 'video.js/dist/video-js.css'

export default {
  data () {
    return {
      usercommented:"",
      userPosts: [],
      logedinUserId: 0,
      knowledgeBaseSearchQuery: '',
      userLatestPhotos: [],
      getFriendRequest: [],
      getComments: [],
      postComments: {
        text:"",
        userId: 0,
        postLogId: 0
      },
      receiveComments: {
        text:"",
        userFirstName: ""
      },
      formData: {
        id: 0,
        userFirstName: "",
        userLastName: "",
        uploadedFileName: "",
      },
      friendRequest: [],
      isNavOpen: false,
    }
  },
  components: {
    videoPlayer
  },
  computed: {
    filteredKB () {
      return this.userPosts.filter((item) => item.caption.toLowerCase().includes(this.knowledgeBaseSearchQuery.toLowerCase()))
    },
    playerOptions () {
      return (media) => {
        return {
          height: '360',
          fluid: true,
          autoplay: false,
          muted: true,
          language: 'en',
          playbackRates: [0.7, 1.0, 1.5, 2.0],
          sources: media.sources,
          poster: media.poster
        }
      }
    }
  },
  methods: {
    downloadImage(img) {
      var self = this;
      window.open(img);
    },
    acceptFriendRequest(friend){
      var self = this;
        self.formData.id = friend.id;
      // self.showLoadingOnElement("whole_page", 0.45, "material");
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Friend Request Accepted",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
        self.$router.push("/pages/friendList")
        
      };
      self.$ajaxPostAnon(self, "Friends/AcceptFriend?id="+ self.formData.id, onSuccess, onFinally);
    },
    getUserInfo() {
      var self = this;
      // self.showLoadingOnElement("whole_page", 1, "material");
      var onSuccess = function (response) {
        debugger
        self.userLatestPhotos = response.data;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"PostLog/GetUserLatestPosts?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    getFriendRequestFromAPI() {
      var self = this;
      // self.showLoadingOnElement("whole_page", 1, "material");
      var onSuccess = function (response) {
        self.getFriendRequest = response.data;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self, "Friends/GetFriendRequest?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    showLoadingOnElement(element, scale, type){
      var self = this;
      self.$vs.loading({
        type: type,
        container: self.$refs[element].$el,
        scale: scale
      });
    },
    loadContent () {
      this.$vs.loading({
        background: this.backgroundLoading,
        color: this.colorLoading,
        container: '#button-load-more-posts',
        scale: 0.45
      })
      setTimeout(() => {
        this.$vs.loading.close('#button-load-more-posts > .con-vs-loading')
      }, 3000)
    },
    getprofileImg(){
      var self = this;
      var onSuccess = function (response) {
        self.formData.userFirstName = response.data.name;
        self.formData.userLastName = response.data.surname;
        self.formData.uploadedFileName = response.data.imageUrl;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"user/GetUserProfile?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    getUserPosts(){
      var self = this;
      var onSuccess = function (response) {
        debugger
        self.userPosts = response.data;
        debugger
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"user/GetUserPostLog?logedinUserId=" + self.logedinUserId, onSuccess, onFinally);
    },
    commentBox(pId){
      var self = this;
      debugger
      self.postComments.userId = parseInt(self.logedinUserId);
      self.postComments.postLogId = pId;
      // self.showLoadingOnElement("whole_page", 0.45, "material");
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Comment posted",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
        self.getUserPosts();
        self.postComments.text = "";
      };
      self.$ajaxPostAnon(self, "Comment/AddComment", self.postComments, onSuccess, onFinally);
    },
    getImageComments(){
      var self = this;
      var onSuccess = function (response) {
        debugger
        self.getComments = response.data;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"Comment/GetComment?postLogId=" + self.logedinUserId, onSuccess, onFinally);
    }
  },
  mounted() {
      var self = this;
      self.logedinUserId = localStorage.getItem("userId");
      self.getUserInfo();
      self.getFriendRequestFromAPI();
      self.getUserPosts();
      // self.getImageComments();
    },
}

</script>


<style lang="scss">
@import "@/assets/scss/vuexy/pages/profile.scss";


.vs-input--placeholder{
  color: white !important;
}
.vs-input--icon{
  color: white !important;
}
.scroll{
  overflow-y: scroll;
}

.theme-dark input {
  background: #10163a !important;
  color: white !important;
}

</style>
