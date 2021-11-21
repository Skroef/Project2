<template>
  <div>
    <vx-card style="background-color: white;">
      <div class="mb-4">
        <vs-button style="border-radius: 9px;" class="button" type="gradient" @click="displaySearch">
          <feather-icon icon="UserPlusIcon" class="align-middle" style="padding-right: inherit;"/>
          <span style="font-size: large; font-weight: 600;" class="align-middle">Invite Friend</span>
        </vs-button>
      </div>
      <div class="vx-row w-full mb-4 ml-0">
        <div >
          <vs-checkbox v-model="following" color="warning">Following</vs-checkbox>
        </div>
        <div>
          <vs-checkbox v-model="followers" color="success">Followers</vs-checkbox>
        </div>
      </div>
      <b-table 
        v-show="getFriends.length != 0"
        :sticky-header="false"
        :no-border-collapse="noCollapse"
        responsive
        :items="getFriends"
        :fields="fields"
      >
        <template #head(id)>
          <div class="text-nowrap">
            Row ID
          </div>
        </template>

        <template #head()="scope">
          <div class="text-nowrap">
            {{ scope.label }}
          </div>
        </template>

        <template #cell(avatar)="data">
          <b-avatar
            class="mr-1"
            :src="data.value"
          />
        </template>

        <!-- <template #cell(status)="data">
          <b-badge :variant="status[1][data.value]">
            {{ status[0][data.value] }}
          </b-badge>
        </template> -->

        <template #cell()="data">
          <span class="text-nowrap">{{ data.value }}</span>
        </template>
      </b-table>
      <vs-divider style="color: grey"></vs-divider>
      <vs-button style="border-radius: 9px;" class="button" @click="goHome">Go Back</vs-button>
    </vx-card>

    <b-modal
        id="move-popup"
        title="Invite a Friend"
        v-model="displaySearchPopUp"
        scrollable
        no-close-on-backdrop
        style="margin-top: 175px;"
        cancel-title="Cancel"
        @cancel="initValues"
        ok-title="Search"
        @ok="searchForFriend"  
        cancel-variant="CancelBTN"
        modal-class="modal-success"
        centered
      >
        <b-card-text style="color:#000000">
          <div class="vx-row">
            <div class="vx-col w-full">
              <h6 class="mb-2" style="color: black">Search for a friend via email</h6>
              <vs-input
                class="w-full"
                v-validate="'required|email'"
                type="email" 
                name="email"
                icon-no-border
                icon="icon icon-user"
                icon-pack="feather"
                placeholder="Email"
                v-model="formData.userEmail"
                />
              <span class="text-danger text-sm">{{ errors.first('email') }}</span>
            </div>
          </div>
        </b-card-text>
      </b-modal>

      <b-modal
        id="move-popup"
        title="Invite a Friend"
        v-model="suggestion"
        scrollable
        no-close-on-backdrop
        style="margin-top: 175px;"
        cancel-title="Cancel"
        @cancel="initValues"
        ok-title="Send Friend Request"
        @ok="sendFriendReq"  

        cancel-variant="CancelBTN"
        modal-class="modal-success"
        centered
      >
      <b-card-text style="color:#000000">
          <div class="vx-row">
            <div class="vx-col w-full">
              <div class="vx-row mt-3">
                <div class="vx-col">
                  <label style="color: balck;"><strong>Profile Image</strong></label>
                  <div class="vx-row mt-2">
                    <div class="vx-col ml-4">
                      <img key="onlineImg" :src="formData.uploadedFileName" alt="user-img" width="40" height="40" class="rounded-full shadow-md block" />
                    </div>
                  </div>
                </div>
                <div class="vx-col">
                  <label style="color: balck;"><strong>Full Name</strong></label>
                  <div class="vx-row mt-2">
                    <div class="vx-col">
                      <span >{{formData.userFirstName + " " + formData.userLastName}}</span>
                    </div>
                  </div>
                </div>
                <div class="vx-col">
                  <label style="color: balck;"><strong>Email</strong></label>
                  <div class="vx-row mt-2">
                    <div class="vx-col">
                      <span >{{formData.userEmail}}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </b-card-text>
      </b-modal>


      

  </div>
</template>

<script>
import { BButton } from 'bootstrap-vue'
import {
  BTable, BFormCheckbox, BAvatar, BBadge, BModal,
} from 'bootstrap-vue'

export default {
  components: {
    BTable,
    BFormCheckbox,
    BAvatar,
    BBadge,
    BButton,
    BModal
  },
  data() {
    return {
      following: false,
      followers: false,
      logedinUserId: 0,
      sendFriend:{
        parentUserId: 0,
        email: "",
      },
      status: "",
      displaySearchPopUp: false,
      noCollapse: false,
      getFriends: [],
      suggestion: false,
      formData: {
        userFirstName: "",
        userLastName: "",
        userEmail: "",
        uploadedFileName: ""
      },
      fields: [
        // {
        //   key: 'id', stickyColumn: true, isRowHeader: true, variant: 'primary',
        // },
        {
          key: 'avatar', label: 'Profile Image',
        },
        {
          key: 'fullName', label: 'Full Name'
        },
        // { key: 'post', stickyColumn: true, variant: 'info' },
        'email',
        { key: 'status', label: 'Status' },
      ],
      items: [
        // {
        //   id: 1,
        //   // eslint-disable-next-line global-require
        //   avatar: this.formData.uploadedFileName,
        //   full_name: this.formData.userFirstName + " " + formData.userLastName,
        //   post: 'Nuclear Power Engineer',
        //   email: 'kocrevy0@thetimes.co.uk',
        //   city: 'Krasnosilka',
        //   start_date: '09/23/2016',
        //   salary: '$23896.35',
        //   age: '61',
        //   experience: '1 Year',
        //   status: 2,
        // },
        // {
        //   id: 2,
        //   // eslint-disable-next-line global-require
        //   avatar: require('@/assets/images/profile/user-uploads/UserProfile.jpg'),
        //   name: 'Bailie Coulman',
        //   post: 'VP Quality Control',
        //   email: 'bcoulman1@yolasite.com',
        //   city: 'Hinigaran',
        //   start_date: '05/20/2018',
        //   salary: '$13633.69',
        //   age: '63',
        //   experience: '3 Years',
        //   status: 2,
        // },
      ],
      // status: [{
      //   1: 'Current', 2: 'Professional', 3: 'Rejected', 4: 'Resigned', 5: 'Applied',
      // },
      // {
      //   1: 'light-primary', 2: 'light-success', 3: 'light-danger', 4: 'light-warning', 5: 'light-info',
      // }],
    }
  },
  watch:{
    following: function() {
      this.getFriendFromAPI();
    },
    followers: function() {
      this.getFriendFromAPI();
    },
  },
  methods:{
    sendFriendReq(){
      var self = this;
      debugger
      // self.showLoadingOnElement("postButton", 0.45, "material");
      self.sendFriend.email = self.formData.userEmail;
      self.sendFriend.parentUserId = parseInt(self.logedinUserId);
      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Friend Request sent",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.postButton.$el);
      };
      self.$ajaxPostAnon(self, "Friends/SendFriendRequest", self.sendFriend, onSuccess, onFinally);
      
    },
    displaySearch(){
      var self = this;
      debugger
      self.displaySearchPopUp = true;
    },
    initValues(){

    },
    showLoadingOnElement(element, scale, type){
      var self = this;
      self.$vs.loading({
        type: type,
        container: self.$refs[element].$el,
        scale: scale
      });
    },
    searchForFriend(){
      var self = this;
      debugger
      self.logedinUserId = localStorage.getItem("userId");
      var onSuccess = function (response) {
        debugger
        // self.users = response.data;
        self.suggestion = true;
        self.displaySearchPopUp = false;
        self.formData.userFirstName = response.data.name;
        self.formData.userLastName = response.data.surname;
        self.formData.uploadedFileName = response.data.imageUrl;
        self.formData.userEmail = response.data.email;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"user/FindFriend?email=" + self.formData.userEmail, onSuccess, onFinally);
    },
    getFriendFromAPI() {
      var self = this;
      self.logedinUserId = localStorage.getItem("userId");
      debugger
      // self.showLoadingOnElement("whole_page", 1, "material");
      var onSuccess = function (response) {
        debugger
        self.getFriends = response.data;
      };
      var onFinally = function () {
        // self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      self.$ajaxGetAnon(self,"Friends/GetFriend?logedinUserId=" + self.logedinUserId + "&following=" + self.following + "&followers=" + self.followers, onSuccess, onFinally);
    },
    goHome(){
      var self = this;
      self.$router.push('/pages/home')
    },
  },
  mounted(){
    var self = this;
    debugger
    self.getFriendFromAPI();
  }
  
}
</script>
<style lang="scss">
.theme-dark label 
{
  color: black !important;
}
.button{
    background: linear-gradient(to left, rgb(46, 89, 132) 0%, rgb(51, 204, 255) 100%) !important;
    color: white !important;
  }
</style>