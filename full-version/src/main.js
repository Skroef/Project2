/*=========================================================================================
  File Name: main.js
  Description: main vue(js) file
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


import Vue from 'vue'
import App from './App.vue'

// Vuesax Component Framework
import Vuesax from 'vuesax'
import 'material-icons/iconfont/material-icons.css' //Material Icons
import 'vuesax/dist/vuesax.css' // Vuesax
Vue.use(Vuesax)


// axios
import axios from './axios.js'
Vue.prototype.$http = axios

// API Calls
import './http/requests'

// mock
import './fake-db/index.js'

// Theme Configurations
import '../themeConfig.js'


// Firebase
import '@/firebase/firebaseConfig'


// Auth0 Plugin
import AuthPlugin from './plugins/auth'
Vue.use(AuthPlugin)


// ACL
import acl from './acl/acl'


// Globally Registered Components
import './globalComponents.js'


// Styles: SCSS
import './assets/scss/main.scss'


// Tailwind
import '@/assets/css/main.css'


// Vue Router
import router from './router'


// Vuex Store
import store from './store/store'


// i18n
import i18n from './i18n/i18n'


// Vuexy Admin Filters
import './filters/filters'

// V-Uploader
// import vUploader from 'v-uploader';
// const uploaderConfig = {
//   uploadFileUrl: process.env.VUE_APP_ROOT_API + "/tempupload",
//   deleteFileUrl: "",
//   showMessage: (vue, message) => {
//     vue.$vs.notify({
//       title: 'Error',
//       color: "danger",
//       text: message,
//       position: 'top-right',
//       iconPack: 'feather',
//       icon: 'icon-thumbs-down'
//     });
//   }
// };
// Vue.use(vUploader, uploaderConfig);

// Clipboard
import VueClipboard from 'vue-clipboard2'
Vue.use(VueClipboard)


// Tour
import VueTour from 'vue-tour'
Vue.use(VueTour)
require('vue-tour/dist/vue-tour.css')


// VeeValidate
import VeeValidate from 'vee-validate'
Vue.use(VeeValidate)


// Google Maps
import * as VueGoogleMaps from 'vue2-google-maps'
Vue.use(VueGoogleMaps, {
  load: {
    // Add your API key here
    key: 'YOUR_KEY',
    libraries: 'places' // This is required if you use the Auto complete plug-in
  }
})

// Vuejs - Vue wrapper for hammerjs
import { VueHammer } from 'vue2-hammer'
Vue.use(VueHammer)


// PrismJS
import 'prismjs'
import 'prismjs/themes/prism-tomorrow.css'

import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue)

// Feather font icon
require('./assets/css/iconfont.css')

Vue.prototype.$VUE_APP_ROOT_API = 'https://localhost:44311/api';
// Vue.prototype.$VUE_APP_ROOT_API = 'https://ninoproject2api.azurewebsites.net/api';

Vue.prototype.$buildUrl = function(path) {
  return this.$VUE_APP_ROOT_API + "/" + path;
}

// Vue select css
// Note: In latest version you have to add it separately
// import 'vue-select/dist/vue-select.css';

Vue.prototype.$getAccessToken = function () {
  return localStorage.getItem("accessToken");
}

Vue.prototype.$getSiteId = function() {
  if(localStorage.getItem('userSiteInfo') != ""){
    if(JSON.parse(localStorage.getItem('userSiteInfo')) instanceof Array)
      return JSON.parse(localStorage.getItem('userSiteInfo'))[0].siteId;
    else 
      return JSON.parse(localStorage.getItem('userSiteInfo')).siteId;
  }
  else
    return 0;
}

Vue.config.productionTip = false

new Vue({
  router,
  store,
  i18n,
  acl,
  render: h => h(App)
}).$mount('#app')

Vue.prototype.$signInWithToken = function (self, email, token, success) {
  var onSuccess = response => {
    if (response.data.apiToken) {
      success(response.data);
    }
  };

  var onError = error => {
    setTimeout(function() {
      window.location.replace("/login");
    }, 2500);
  }
  
  this.$ajaxPostAnon(self, "Authentication/SignInWithToken", { Email: email, Token: token }, onSuccess, function() {}, onError);
}

Vue.prototype.$signIn = function (self, email, password, success) {
  var onSuccess = response => {
    if (response.data.apiToken) {
      success(response.data);
    }
  };
  
  this.$ajaxPostAnon(self, "Authentication/SignIn", { Email: email, Password: CryptoJs.MD5(password).toString() }, onSuccess);
}

Vue.prototype.$ajaxGet = function (self, myUrl, onSuccess, onFinally) {
  var mySelf = this;
  var myAccessToken = mySelf.$getAccessToken().toString();
  // var mySiteId = mySelf.$getSiteId();
  return axios({
    method: 'get',
    url: mySelf.$buildUrl(myUrl),
      headers: { 
	      'Authorization': 'Bearer ' + myAccessToken, 
	// 	'siteId': mySiteId 
      }
  }).then(response => {
    if (onSuccess && typeof onSuccess == "function")
      onSuccess(response);
  })
    .catch(function (error) {
      var exception = "";
      var colour = "danger";
      if (error.response) {
        if (error.response.status == 401) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 405) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 498) {
          localStorage.setItem("accessToken","");
          router.push('/login');
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 500) {
          exception = error.response.data.message;
          colour = "danger";
        }
      } else {
        exception = error.message;
        colour = "danger";
      }
      mySelf.$vs.notify({
        time: 6000,
        title: 'Error',
        text: exception,
        color: colour,
        iconPack: 'feather',
        icon: 'icon-alert-circle'
      });
    })
    .finally(onFinally)
}

Vue.prototype.$ajaxGetAnon = function (self, myUrl, onSuccess, onFinally) {
  var mySelf = this;
  return axios({
    method: 'get',
    url: mySelf.$buildUrl(myUrl),
   }).then(response => {
    if (onSuccess && typeof onSuccess == "function")
      onSuccess(response);
  })
    .catch(function (error) {
      var exception = "";
      var colour = "danger";
      if (error.response) {
        if (error.response.status == 401) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 405) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 498) {
          localStorage.setItem("accessToken", "");
          router.push('/login');
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 500) {
          exception = error.response.data.message;
          colour = "danger";
        }
      } else {
        exception = error.message;
        colour = "danger";
      }
      mySelf.$vs.notify({
        time: 6000,
        title: 'Error',
        text: exception,
        color: colour,
        iconPack: 'feather',
        icon: 'icon-alert-circle'
      });
    })
    .finally(onFinally);
}

Vue.prototype.$ajaxPost = function (self, myUrl, formData, onSuccess, onFinally) {
  var mySelf = this;
    var myAccessToken = mySelf.$getAccessToken();
  // var mySiteId = mySelf.$getSiteId();
  return axios({
    method: 'post',
    url: mySelf.$buildUrl(myUrl),
    data: formData,
    headers: { 
		  'Authorization': 'Bearer ' + myAccessToken, 
		// 'siteId': mySiteId 
  }
  }).then(response => {
    if (onSuccess && typeof onSuccess == "function")
      onSuccess(response);
  })
    .catch(function (error) {
      var exception = "";
      var colour = "danger";
      if (error.response) {
        if (error.response.status == 401) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 405) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 498) {
          localStorage.setItem("accessToken", "");
          router.push('/login');
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 500) {
          exception = error.response.data.message;
          colour = "danger";
        }
      } else {
        exception = error.message;
        colour = "danger";
      }
      mySelf.$vs.notify({
        time: 6000,
        title: 'Error',
        text: exception,
        color: colour,
        iconPack: 'feather',
        icon: 'icon-alert-circle'
      });
    })
    .finally(onFinally);
}

Vue.prototype.$ajaxPostAnon = function (self, myUrl, formData, onSuccess, onFinally, onError) {
  var mySelf = this;
  return axios({
    method: 'post',
    url: mySelf.$buildUrl(myUrl),
    data: formData,
  }).then(response => {
    if (onSuccess && typeof onSuccess == "function")
      onSuccess(response);
  })
    .catch(function (error) {
      var exception = "";
      var colour = "danger";
      if (error.response) {
        if (error.response.status == 401) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 405) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 498) {
          localStorage.setItem("accessToken", "");
          router.push('/login');
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 500) {
          exception = error.response.data.message;
          colour = "danger";
        }
      } else {
        exception = error.message;
        colour = "danger";
      }
      mySelf.$vs.notify({
        time: 6000,
        title: 'Error',
        text: exception,
        color: colour,
        iconPack: 'feather',
        icon: 'icon-alert-circle'
      });
      onError(error);
    })
    .finally(onFinally);
}

Vue.prototype.$ajaxDelete = function (self, myUrl, onSuccess, onFinally) {
  var mySelf = this;
    var myAccessToken = mySelf.$getAccessToken();
  // var mySiteId = mySelf.$getSiteId();
  return axios({
    method: 'delete',
    url: mySelf.$buildUrl(myUrl),
    headers: { 
		  'Authorization': 'Bearer ' + myAccessToken, 
		// 'siteId': mySiteId 
  }
  }).then(response => {
    if (onSuccess && typeof onSuccess == "function")
      onSuccess(response);
  })
    .catch(function (error) {
      var exception = "";
      var colour = "danger";
      if (error.response) {
        if (error.response.status == 401) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 405) {
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 498) {

          localStorage.setItem("accessToken", "");
          router.push('/login');
          exception = error.response.data.message;
          colour = "warning";
        } else if (error.response.status == 500) {
          exception = error.response.data.message;
          colour = "danger";
        }
      } else {
        exception = error.message;
        colour = "danger";
      }
      mySelf.$vs.notify({
        time: 6000,
        title: 'Error',
        text: exception,
        color: colour,
        iconPack: 'feather',
        icon: 'icon-alert-circle'
      });
    })
    .finally(onFinally);
}