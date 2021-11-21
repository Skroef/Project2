<template>
<div class="relative">
  <div class="vx-navbar-wrapper navbar-full p-0">
    <vs-navbar class="navbar-custom navbar-skelton" style="color: white" :class="navbarClasses"  :style="navbarStyle" :color="navbarColor">
      <router-link tag="div" to="/pages/home" class="vx-logo cursor-pointer mx-auto flex items-center">
        <img style="height: 98px; margin-left: 194px;;" src="@/assets/images/logo/Project2LogoMain.png" alt="login" />
      </router-link>
      <profile-drop-down />
    </vs-navbar>
  </div>
</div>
</template>

<script>
import Bookmarks            from './components/Bookmarks.vue'
import I18n                 from './components/I18n.vue'
import SearchBar            from './components/SearchBar.vue'
import CartDropDown         from './components/CartDropDown.vue'
import NotificationDropDown from './components/NotificationDropDown.vue'
import ProfileDropDown      from './components/ProfileDropDown.vue'
import Logo                 from '../Logo.vue'

export default {
  name: 'the-navbar-horizontal',
  props: {
    logo: { type: String},
    navbarType: {
      type: String,
      required: true
    }
  },
  components: {
    Logo,
    Bookmarks,
    I18n,
    SearchBar,
    CartDropDown,
    NotificationDropDown,
    ProfileDropDown
  },
  computed: {
    navbarColor () {
      let color = '#fff';
      color = 'linear-gradient(to left, #2e5984 0%, #33ccff 100%)';

      return color
    },
    isThemedark ()          { return this.$store.state.theme                                                                       },
    navbarStyle ()          { return this.navbarType === 'static' ? {transition: 'all .25s ease-in-out'} : {}                      },
    navbarClasses ()        { return this.scrollY > 5 && this.navbarType === 'static' ? null : 'd-theme-dark-light-bg shadow-none' },
    scrollY ()              { return this.$store.state.scrollY                                                                     },
    verticalNavMenuWidth () { return this.$store.state.verticalNavMenuWidth                                                        },
    windowWidth ()          { return this.$store.state.windowWidth                                                                 }
  }
}

</script>

