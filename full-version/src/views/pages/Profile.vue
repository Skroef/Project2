<template>
  <div ref="whole_page">
    <div ref="profilerow" class="vx-row vs-con-loading__container">
      <div class="vx-col w-1/6 profile-container cursor-pointer" @click="showUpload">
        <div class="text-center">
          <img id="profileimg" v-if="formData.uploadedFileName" :src="formData.uploadedFileName" width="100%" class="cursor-pointer">
          <div v-if="formData.uploadedFileName" class="profile-overlay cursor-pointer">
            <div class="profile-overlay-text">Click to change image</div>
          </div>
          <div v-if="formData.uploadedFileName == null">
              <file-pond
              :server="uploadLocation"
              label-idle="Drop picture here or <span class='filepond--label-action' text-decoration-color: #aaa>Browse</span>"
              accepted-file-types="image/*"
              :allow-multiple="false"
              :instant-upload="true"
              @processfile="profilePicUploadComplete"
              @removefile="profilePicRemoved"
            />
            <vs-divider class="pt-5" position="center">OR</vs-divider>
            <web-cam
              ref="webcam"
              height="175px"
              :deviceId="deviceId"
              @success="onSuccess"
              @cameras="onCameras"
              :isFrontCam="true"
              :debug="false"
            />
            <div class="vx-row">
              <div class="vx-col w-full" style="text-align: -webkit-center;">
                <vs-button size="small" icon-pack="feather" icon="icon-camera" color="success" @click="onCapture">Capture</vs-button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="vx-row pb-3 ml-5">
        <div class="vx-col w-full">
          <h2>{{ formData.userFirstName }} {{ formData.userLastName }}</h2>
          <h5 class="pt-4">Email: <strong>{{ formData.email }}</strong></h5>
          <h5 class="pt-4">Mobile: <strong>{{ formData.mobile }}</strong></h5>
        </div>
      </div>
    </div>
    <vs-divider class="mb-6">User Info</vs-divider>
    <div class="vx-row pb-3">
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <label style="color: white"><strong>First Name</strong></label>
        <vs-input autocomplete="chrome-off" onClick="this.select();" class="w-full" v-validate="'required'" name="First Name" icon-pack="feather" icon="icon-user" icon-no-border v-model="formData.userFirstName" />
        <span class="text-danger" style="font-weight: bold;" v-show="errors.has('First Name')">{{ errors.first('First Name') }}</span>
      </div>
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <label style="color: white"><strong>Last Name</strong></label>
        <vs-input autocomplete="chrome-off" onClick="this.select();" class="w-full" v-validate="'required'" name="Last Name" icon-pack="feather" icon="icon-user" icon-no-border v-model="formData.userLastName" />
        <span class="text-danger" style="font-weight: bold;" v-show="errors.has('Last Name')">{{ errors.first('Last Name') }}</span>
      </div>
    </div>

    <div class="vx-row pb-3">
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <label style="color: white"><strong>Mobile</strong></label>
        <vs-input  onClick="this.select();" class="w-full" v-validate="'required|digits:10'" name="Mobile" icon-pack="feather" icon="icon-smartphone" icon-no-border v-model="formData.mobile" />
        <span class="text-danger" style="font-weight: bold;" v-show="errors.has('Mobile')">{{ errors.first('Mobile') }}</span>
      </div>
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <label style="color: white"><strong>Email</strong></label>
        <vs-input autocomplete="chrome-off" ref="emailInput" onClick="this.select();" type="required|email" v-validate="'email'" name="email" class="w-full" icon-pack="feather" icon="icon-at-sign" v-model="formData.email" />
        <span class="text-danger" style="font-weight: bold;" v-show="errors.has('email')">{{ errors.first('email') }}</span>
      </div>
    </div>
          
    <!-- Manage Password -->
    <vs-divider class="mb-6 pt-0">Login Credentials</vs-divider>
    <div class="vx-row pb-3">
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <vs-checkbox style="color: white" v-model="changePassword">Change Password</vs-checkbox>
      </div>
    </div>
    <div class="vx-row pb-3" v-show="changePassword">
      <div class="vx-col sm:w-1/2 w-full mb-2">
        <label>Password</label>
        <vs-input autocomplete="chrome-off" onClick="this.select();" type="password" v-validate="'min:6|max:100'" name="password" ref="password" class="w-full" icon-pack="feather" icon="icon-lock" icon-no-border v-model="formData.password" />
        <span class="text-danger text-sm" v-show="errors.has('password')">{{ errors.first('password') }}</span>
      </div>
    </div>
    <div class="vx-row pb-3" v-show="changePassword">
      <div class="vx-col sm:w-1/2 w-full mb-2" v-show="formData.password" >
        <label>Confirm Password</label>
        <vs-input autocomplete="chrome-off" onClick="this.select();" type="password" v-validate="'min:6|max:100|confirmed:password'" name="confirm_password" data-vv-as="password" class="w-full" icon-pack="feather" icon="icon-lock" icon-no-border v-model="formData.passwordConfirm" />
        <span class="text-danger text-sm" v-show="errors.has('confirm_password')">{{ errors.first('confirm_password') }}</span>
      </div>
    </div>
    
    <div class="vx-row">
      <vs-spacer></vs-spacer>
      <div class="vx-col">
        <vs-button class="vs-con-loading__container" color="success" type="filled" size="large" ref="saveButton" @click="saveChanges">Save Changes</vs-button>
      </div>
    </div>

    <!-- <vs-divider>Required Documents</vs-divider>
    <div class="vx-row">
      <div class="vx-col w-full">
        <vs-table stripe ref="documentsTable" class="vs-con-loading__container"  search :data="uploadedDocuments">

          <div slot="header" class="flex flex-wrap-reverse items-center flex-grow justify-between">

            <div class="flex flex-wrap-reverse items-center">
              <div class="p-3 mr-4 rounded-lg cursor-pointer flex items-center justify-between text-lg font-medium text-base text-primary-vuexy border border-solid border-primary-vuexy" @click="getDocuments">
                <feather-icon icon="RefreshCcwIcon" svgClasses="h-4 w-4" />
                <span class="ml-2 text-base text-primary-vuexy">Refresh</span>
              </div>
            </div>
          </div>

          <template slot="thead">
            <vs-th sort-key="typeName">Type</vs-th>
            <vs-th sort-key="statusName">Status</vs-th>
            <vs-th sort-key="comments">Comments</vs-th>
            <vs-th></vs-th>
          </template>

          <template slot-scope="{data}">
            <vs-tr :data="file" :key="index" v-for="(file, index) in data" >

              <vs-td :data="data[index].typeName">
                {{ file.typeName }}
              </vs-td>

              <vs-td :data="data[index].status">
                <vs-chip :color="getStatusColor(file.statusId)">{{ getStatusName(file.statusId, file.statusName) }}</vs-chip>
              </vs-td>

              <vs-td :data="data[index].comments">
                {{ file.comments || 'N/A' }}
              </vs-td>

              <vs-td>
                <vs-button v-if="!file.statusId || file.statusId == 3" color="success" type="flat" size="large" icon-pack="feather" icon="icon-upload-cloud" @click.stop.prevent="showUploadDialog(file.typeId, file.id)"/>
              </vs-td>

            </vs-tr>
          </template>
        </vs-table>
      </div>
    </div> -->

    <!-- <file-pond
      v-show="false"
      class="vs-con-loading__container"
      id="financepond"
      ref="documentpond"
      :server="uploadLocation"
      :label-idle="`Drop <strong>new</strong> file here or <span class='filepond--label-action'>Browse</span>`"
      :allow-multiple="false"
      :instant-upload="true"
      :allow-image-preview="false"
      @addfilestart="beforeUpload"
      @processfilestart="startUpload"
      @processfileprogress="uploadProgress"
      @processfile="docTempUploadComplete"
    /> -->
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
      uploadedDocuments: [],
      formData: {
        id: 0,
        userFirstName: "",
        userLastName: "",
        mobile: "",
        email: "",
        uploadedFileName: "",
        password: "",
        passwordConfirm: ""
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
      // script: [{
      //   src: `https://maps.googleapis.com/maps/api/js?key=${this.$VUE_APP_GOOGLE_MAPS_API_KEY}&libraries=places`,
      //   skaap: `https://maps.googleapis.com/maps/api/place/autocomplete/json?types=${this.formData.cities}&key=${this.$VUE_APP_GOOGLE_MAPS_API_KEY}`,
      //   async: true,
      //   defer: true,
      //   callback: () => this.mapsInit() // will declare it in methods
      //   ,componentRestrictions: {country: "za"}
      // }]
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
    mapsInit () {
      this.service = new window.google.maps.places.AutocompleteService()
    },
    displaySuggestions(predictions, status) {
      if(predictions)
      {
        var filteredPredictions = predictions.filter(x=> x.terms.length > 2);

        if (status !== window.google.maps.places.PlacesServiceStatus.OK) {
          this.searchResults = []
          return
        }
        this.searchResults = filteredPredictions;
      
      }
    },
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
    getUserInfo() {
      var self = this;
      self.showLoadingOnElement("whole_page", 1, "material");

      var onSuccess = function (response) {
        self.changePassword = false;
        self.formData.id = response.data.id;
        self.formData.userFirstName = response.data.name;
        self.formData.userLastName = response.data.surname;
        self.formData.mobile = response.data.mobileNumber;
        self.formData.uploadedFileName = response.data.imageUrl;
        self.formData.email = response.data.email;

        // self.getDocuments();

      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.whole_page.$el);
      };
      var id = localStorage.getItem("userId");
      self.$ajaxGetAnon(self,"user/GetCurrentUserInfo?id=" + id, onSuccess, onFinally);
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
    saveChanges() {
      var self = this;
      self.showLoadingOnElement("saveButton", 0.45, "material");
      var routeData = null;
      var pack = {
        id: this.formData.id,
        password: this.formData.password,
        name: this.formData.userFirstName,
        surName: this.formData.userLastName,
        email: this.formData.email,
        mobileNumber: this.formData.mobile,
        imageUrl: this.formData.uploadedFileName,
      };

      var onSuccess = response => {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "Changes Saved",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-save'
        });

        // if(response.data.response){
        //   localStorage.clear();
        //   sessionStorage.clear();
        //   routeData = self.$router.resolve({ name: 'Login' });
        //   window.open(routeData.href, '_self');
        // }
      };
      var onFinally = function () {
        self.$vs.loading.close(self.$refs.saveButton.$el);
        self.$router.push('/pages/home');
      };
      self.$ajaxPostAnon(self, "User/EditUser", pack, onSuccess, onFinally);
    },
    // getDocuments() {
    //   var self = this;

    //   self.showLoadingOnElement("documentsTable", 1, "material");

    //   var onSuccess = response => {
    //     self.uploadedDocuments = response.data;
    //   };
    //   var onFinally = function () {
    //     self.$vs.loading.close(self.$refs.documentsTable.$el);
    //   };

    //   self.$ajaxGet(self, "Spotter/GetDocuments", onSuccess, onFinally);
    // },
    getStatusColor(id) {
      if (id) {
        if (id == 1 || id == 4) {
          return "primary"
        }
        else if (id == 2) {
          return "success"
        }
        else if (id == 3) {
          return "danger"
        }
      } else {
        return "warning"
      }
    },
    getStatusName(statusId, statusName) {
      if (statusId) {
        if (statusId == 1) {
          return "Waiting for review..."
        }
        else {
          return statusName;
        }
      } 
      else {
        return "Not Uploaded";
      }
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
    // uploadDocument(tempUrl) {
    //   var self = this;

    //   self.showLoadingOnElement("documentsTable", 1, "material");

    //   var docToUpload = {
    //     id: self.currentDocumentId,
    //     url: tempUrl,
    //     typeId: self.currentDocumentToUploadType
    //   }

    //   var onSuccess = response => {
    //     self.$refs.documentpond.removeFiles();
    //     setTimeout(function() {
    //       self.getDocuments();
    //     }, 150)
    //   };
    //   var onFinally = function () {
    //     self.$vs.loading.close(self.$refs.documentsTable.$el);
    //   };

    //   self.$ajaxPost(self, "Spotter/UploadDocument", docToUpload, onSuccess, onFinally);
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
  },
  mounted() {
    // this.scrollToNavbar();
    this.getUserInfo();
  },
}
</script>
<style lang="scss">
  #profileimg {
    border-radius: 50%;
    padding:2px;
    border:1px solid #838383;
    background-color:rgb(255, 255, 255);
    // /* max-width: 100px; */
    object-fit: cover !important;
    background-color: white !important;
    width: 282px !important;
    height: 282px !important;
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
}
</style>


