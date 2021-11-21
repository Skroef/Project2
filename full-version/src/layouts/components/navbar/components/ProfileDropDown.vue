<template>
  <div class="the-navbar__user-meta flex items-center" >
    <div class="text-right leading-tight hidden sm:block">
      <p class="font-semibold ml-2">{{ formData.userFirstName + " " + formData.userLastName }}</p>
      <!-- <small>Available</small> -->
    </div>
    <vs-dropdown vs-custom-content vs-trigger-click class="cursor-pointer">
      <div class="con-img ml-3">
        <img key="onlineImg" :src="formData.uploadedFileName" alt="user-img" width="60" height="60" class="rounded-full shadow-md cursor-pointer block" />
      </div>
      <vs-dropdown-menu class="vx-navbar-dropdown">
        <ul style="min-width: 9rem">
          <li
            class="flex py-2 px-4 cursor-pointer hover:bg-primary hover:text-white"
            @click="viewProfile">
            <feather-icon icon="UserIcon" svgClasses="w-4 h-4" />
            <span class="ml-2">View Profile</span>
          </li>
          <vs-divider class="m-1"></vs-divider>
          <li
            class="flex py-2 px-4 cursor-pointer hover:bg-primary hover:text-white"
            @click="logout">
            <feather-icon icon="LogOutIcon" svgClasses="w-4 h-4" />
            <span class="ml-2">Logout</span>
          </li>
          
        </ul>
      </vs-dropdown-menu>
    </vs-dropdown>
  </div>
</template>

<script>
import firebase from 'firebase/app'
import 'firebase/auth'

export default {
  data () {
    return {
      logedinUserId: 0,
      formData: {
        userFirstName: "",
        userLastName: "",
        uploadedFileName: "",
      },
    }
  },
  computed: {
    activeUserInfo () {
      return this.$store.state.AppActiveUser
    }
  },
  methods: {
    logout () {
      // if user is logged in via auth0
      if (this.$auth.profile) this.$auth.logOut()

      // if user is logged in via firebase
      const firebaseCurrentUser = firebase.auth().currentUser

      if (firebaseCurrentUser) {
        firebase.auth().signOut().then(() => {
          this.$router.push('/pages/login').catch(() => {})
        })
      }
      // If JWT login
      if (localStorage.getItem('accessToken')) {
        localStorage.removeItem('accessToken')
        this.$router.push('/pages/login').catch(() => {})
      }

      // Change role on logout. Same value as initialRole of acj.js
      this.$acl.change('Editor')
      localStorage.removeItem('userInfo')

      // This is just for demo Purpose. If user clicks on logout -> redirect
      this.$router.push('/pages/login').catch(() => {})
    },
    viewProfile() {
      var routeData = this.$router.resolve({ name: 'page-profile' });
      window.open(routeData.href, '_blank');
    },
    getprofileImg(){
      var self = this;
      var onSuccess = function (response) {
        self.formData.userFirstName = response.data.name;
        self.formData.userLastName = response.data.surname;
        self.formData.uploadedFileName = response.data.imageUrl;
        debugger
        if(self.formData.uploadedFileName == "" || self.formData.uploadedFileName == null){
          self.formData.uploadedFileName = "https://forty5degreesstorage.blob.core.windows.net/user/avatar_default.png";
        }
        else{
          self.formData.uploadedFileName = response.data.imageUrl;
        }
      };
      self.$ajaxGetAnon(self,"user/GetUserProfileImg?logedinUserId=" + self.logedinUserId, onSuccess);
    },
  },
  mounted() {
    var self = this;
    self.logedinUserId = localStorage.getItem("userId");
    self.getprofileImg();
  },
}
</script>
