/*=========================================================================================
  File Name: moduleAuthActions.js
  Description: Auth Module Actions
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

import jwt from '../../http/requests/auth/jwt/index.js'


import firebase from 'firebase/app'
import 'firebase/auth'
import router from '@/router'

export default {
  adminLogin({
    commit,
    state
  }, payload) { //not used anymore
    localStorage.setItem('permissions', '');
    localStorage.setItem('userSiteInfo', '');
    localStorage.setItem('Current_User_Id', '');

    if (state.isUserLoggedIn()) {
      if (payload.self.$refs.login)
        payload.self.$vs.loading.close(payload.self.$refs.login.$el);
      payload.notify({
        title: 'Login Attempt',
        text: 'You are already logged in!',
        iconPack: 'feather',
        icon: 'icon-alert-circle',
        color: 'warning'
      });
      return false
    }
    payload.self.$signIn(payload.self, payload.userDetails.email, payload.userDetails.password, (result) => {
      var data = result;
      var localStorageString = JSON.stringify(data);
      localStorage.setItem('userInfo', localStorageString);

      localStorage.setItem('Current_User_Id', data.currentUserId);
      localStorage.setItem('DisplayName', data.displayName);
      localStorage.setItem("accessToken", data.apiToken);

      // localStorage.setItem('permissions', 'admin')


    });
  },
  login({
    commit,
    state
  }, payload) {
    localStorage.setItem('permissions', '');
    localStorage.setItem('userSiteInfo', '');
    localStorage.setItem('Current_User_Id', '');
    
    if (state.isUserLoggedIn()) {
      if (payload.self.$refs.login)
        payload.self.$vs.loading.close(payload.self.$refs.login.$el);
      payload.notify({
        title: 'Login Attempt',
        text: 'You are already logged in!',
        iconPack: 'feather',
        icon: 'icon-alert-circle',
        color: 'warning'
      });
      return false
    }

    var success = (result) => {

      //Set userInfo in localStorage
      var data = result;
      var localStorageString = JSON.stringify(data);
      localStorage.setItem('userInfo', localStorageString);
      localStorage.setItem("accessToken", data.apiToken);

      localStorage.setItem('Current_User_Id', data.currentUserId);
      localStorage.setItem('DisplayName', data.displayName);

      // localStorage.setItem('permissions', 'admin');



    }

    if (payload.userDetails.token) {
      payload.self.$signInWithToken(payload.self, payload.userDetails.email, payload.userDetails.token, success);
    } else {
      payload.self.$signIn(payload.self, payload.userDetails.email, payload.userDetails.password, success);
    }

  },

  updateAuthenticatedUser({
    commit
  }, payload) {
    commit('UPDATE_AUTHENTICATED_USER', payload)
    payload.self.$vs.loading.close(payload.self.$refs.login.$el);
  }
}
