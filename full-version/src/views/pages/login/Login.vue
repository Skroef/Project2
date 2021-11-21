<template>
  <div class="flex w-full items-center justify-center login-background" id="login-background" style="padding-top: 70px;" >
    <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-3/5 sm:m-0 m-4">
      <div slot="no-body" >
        <div class="vx-row justify-center items-center" >
          <img src="@/assets/images/logo/Project2LogoMain.png" alt="login" style="height: 200px; width: 286px;">
        </div>
        <div class="vx-row justify-center items-center" >
          <div class="vx-col sm:w-full md:w-full lg:w-1/2 ">
            <div class="px-8 pt-8 login-tabs-container">
              <div class="vx-row justify-center items-center">
                <h3 class="mb-4 mt-5"><strong>Login</strong></h3>
              </div>
              <div>
                <label style="color: white"><strong>Email</strong></label>
              </div>
              <vs-input
                class="w-full"
                v-validate="'required|email'"
                type="email" 
                name="email"
                icon-no-border
                icon="icon icon-user"
                icon-pack="feather"
                placeholder="Email"
                v-model="email"
                style="color: black"
                />
              <span class="text-danger text-sm">{{ errors.first('email') }}</span>
              <div>
                <label class="mt-4" style="color: white"><strong>Password</strong></label>
              </div>
              <vs-input
                class="w-full"
                data-vv-validate-on="blur"
                v-validate="'required|min:6|max:10'"
                type="password"
                name="password"
                icon-no-border
                icon="icon icon-lock"
                icon-pack="feather"
                placeholder="Password"
                v-model="password"
                />
              <span class="text-danger text-sm">{{ errors.first('password') }}</span>
              <!-- <div class="flex flex-wrap justify-between my-5">
                  <vs-checkbox v-model="checkbox_remember_me" class="mb-3">Remember Me</vs-checkbox>
                  <router-link to="/pages/forgot-password">Forgot Password?</router-link>
              </div> -->
              <div class="flex flex-wrap justify-between mt-4 mb-3">
                <vs-button style="background-color: #2E5984 !important; color: white;" v-ripple.400="'rgba(255, 255, 255, 0.15)'" @click="registerUser">Register</vs-button>
                <vs-button style="background-color: #2E5984 !important; color: white;" :disabled="!validateForm" @click="loginJWT">Login</vs-button>
              </div>
              <!-- <vs-tabs>
                <vs-tab label="JWT">
                  <login-jwt></login-jwt>
                </vs-tab>

                <vs-tab label="Firebase">
                  <login-firebase></login-firebase>
                </vs-tab>

                <vs-tab label="Auth0">
                  <login-auth0></login-auth0>
                </vs-tab>
              </vs-tabs> -->
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import LoginJwt from './LoginJWT.vue'
import LoginFirebase from './LoginFirebase.vue'
import LoginAuth0 from './LoginAuth0.vue'
import VueCryptojs from 'vue-cryptojs'


export default {
  components: {
    LoginJwt,
    LoginFirebase,
    LoginAuth0
  },
  computed: {
    validateForm () {
      return !this.errors.any() && this.email !== '' && this.password !== ''
    }
  },
  data () {
    return {
      email: '',
      password: '',
      checkbox_remember_me: false
    }
  },
  methods: {
    checkLogin () {
      // If user is already logged in notify
      if (this.$store.state.auth.isUserLoggedIn()) {
        // Close animation if passed as payload
        // this.$vs.loading.close()
        this.$vs.notify({
          title: 'Login Attempt',
          text: 'You are already logged in!',
          iconPack: 'feather',
          icon: 'icon-alert-circle',
          color: 'warning'
        })
        debugger
        this.$router.push('/pages/home');
        return false
      }
      return true
    },
    loginJWT () {
      var self = this;
      localStorage.clear();
      debugger
      if (!self.checkLogin()) return
      var onSuccess = function (response) {
        localStorage.setItem("accessToken", response.data.value.mtoken);
        localStorage.setItem("userInfo", JSON.stringify(response.data.value.authInfo));
        localStorage.setItem("userId",response.data.value.userId);
        self.$router.push('/pages/home');
        localStorage.getItem("userId");
      };
      var hash = self.md5(self.password);
      console.log(hash.toString()); 
      debugger
      self.$ajaxGetAnon(self, "Auth/AuthUser?password=" + hash + "&email=" + self.email, onSuccess);
    },
    registerUser () {
      if (!this.checkLogin()) return
      this.$router.push('/pages/register').catch(() => {})
    },
    md5(inputString) {
    var hc="0123456789abcdef";
    function rh(n) {var j,s="";for(j=0;j<=3;j++) s+=hc.charAt((n>>(j*8+4))&0x0F)+hc.charAt((n>>(j*8))&0x0F);return s;}
    function ad(x,y) {var l=(x&0xFFFF)+(y&0xFFFF);var m=(x>>16)+(y>>16)+(l>>16);return (m<<16)|(l&0xFFFF);}
    function rl(n,c)            {return (n<<c)|(n>>>(32-c));}
    function cm(q,a,b,x,s,t)    {return ad(rl(ad(ad(a,q),ad(x,t)),s),b);}
    function ff(a,b,c,d,x,s,t)  {return cm((b&c)|((~b)&d),a,b,x,s,t);}
    function gg(a,b,c,d,x,s,t)  {return cm((b&d)|(c&(~d)),a,b,x,s,t);}
    function hh(a,b,c,d,x,s,t)  {return cm(b^c^d,a,b,x,s,t);}
    function ii(a,b,c,d,x,s,t)  {return cm(c^(b|(~d)),a,b,x,s,t);}
    function sb(x) {
        var i;var nblk=((x.length+8)>>6)+1;var blks=new Array(nblk*16);for(i=0;i<nblk*16;i++) blks[i]=0;
        for(i=0;i<x.length;i++) blks[i>>2]|=x.charCodeAt(i)<<((i%4)*8);
        blks[i>>2]|=0x80<<((i%4)*8);blks[nblk*16-2]=x.length*8;return blks;
    }
    var i,x=sb(inputString),a=1732584193,b=-271733879,c=-1732584194,d=271733878,olda,oldb,oldc,oldd;
    for(i=0;i<x.length;i+=16) {olda=a;oldb=b;oldc=c;oldd=d;
        a=ff(a,b,c,d,x[i+ 0], 7, -680876936);d=ff(d,a,b,c,x[i+ 1],12, -389564586);c=ff(c,d,a,b,x[i+ 2],17,  606105819);
        b=ff(b,c,d,a,x[i+ 3],22,-1044525330);a=ff(a,b,c,d,x[i+ 4], 7, -176418897);d=ff(d,a,b,c,x[i+ 5],12, 1200080426);
        c=ff(c,d,a,b,x[i+ 6],17,-1473231341);b=ff(b,c,d,a,x[i+ 7],22,  -45705983);a=ff(a,b,c,d,x[i+ 8], 7, 1770035416);
        d=ff(d,a,b,c,x[i+ 9],12,-1958414417);c=ff(c,d,a,b,x[i+10],17,     -42063);b=ff(b,c,d,a,x[i+11],22,-1990404162);
        a=ff(a,b,c,d,x[i+12], 7, 1804603682);d=ff(d,a,b,c,x[i+13],12,  -40341101);c=ff(c,d,a,b,x[i+14],17,-1502002290);
        b=ff(b,c,d,a,x[i+15],22, 1236535329);a=gg(a,b,c,d,x[i+ 1], 5, -165796510);d=gg(d,a,b,c,x[i+ 6], 9,-1069501632);
        c=gg(c,d,a,b,x[i+11],14,  643717713);b=gg(b,c,d,a,x[i+ 0],20, -373897302);a=gg(a,b,c,d,x[i+ 5], 5, -701558691);
        d=gg(d,a,b,c,x[i+10], 9,   38016083);c=gg(c,d,a,b,x[i+15],14, -660478335);b=gg(b,c,d,a,x[i+ 4],20, -405537848);
        a=gg(a,b,c,d,x[i+ 9], 5,  568446438);d=gg(d,a,b,c,x[i+14], 9,-1019803690);c=gg(c,d,a,b,x[i+ 3],14, -187363961);
        b=gg(b,c,d,a,x[i+ 8],20, 1163531501);a=gg(a,b,c,d,x[i+13], 5,-1444681467);d=gg(d,a,b,c,x[i+ 2], 9,  -51403784);
        c=gg(c,d,a,b,x[i+ 7],14, 1735328473);b=gg(b,c,d,a,x[i+12],20,-1926607734);a=hh(a,b,c,d,x[i+ 5], 4,    -378558);
        d=hh(d,a,b,c,x[i+ 8],11,-2022574463);c=hh(c,d,a,b,x[i+11],16, 1839030562);b=hh(b,c,d,a,x[i+14],23,  -35309556);
        a=hh(a,b,c,d,x[i+ 1], 4,-1530992060);d=hh(d,a,b,c,x[i+ 4],11, 1272893353);c=hh(c,d,a,b,x[i+ 7],16, -155497632);
        b=hh(b,c,d,a,x[i+10],23,-1094730640);a=hh(a,b,c,d,x[i+13], 4,  681279174);d=hh(d,a,b,c,x[i+ 0],11, -358537222);
        c=hh(c,d,a,b,x[i+ 3],16, -722521979);b=hh(b,c,d,a,x[i+ 6],23,   76029189);a=hh(a,b,c,d,x[i+ 9], 4, -640364487);
        d=hh(d,a,b,c,x[i+12],11, -421815835);c=hh(c,d,a,b,x[i+15],16,  530742520);b=hh(b,c,d,a,x[i+ 2],23, -995338651);
        a=ii(a,b,c,d,x[i+ 0], 6, -198630844);d=ii(d,a,b,c,x[i+ 7],10, 1126891415);c=ii(c,d,a,b,x[i+14],15,-1416354905);
        b=ii(b,c,d,a,x[i+ 5],21,  -57434055);a=ii(a,b,c,d,x[i+12], 6, 1700485571);d=ii(d,a,b,c,x[i+ 3],10,-1894986606);
        c=ii(c,d,a,b,x[i+10],15,   -1051523);b=ii(b,c,d,a,x[i+ 1],21,-2054922799);a=ii(a,b,c,d,x[i+ 8], 6, 1873313359);
        d=ii(d,a,b,c,x[i+15],10,  -30611744);c=ii(c,d,a,b,x[i+ 6],15,-1560198380);b=ii(b,c,d,a,x[i+13],21, 1309151649);
        a=ii(a,b,c,d,x[i+ 4], 6, -145523070);d=ii(d,a,b,c,x[i+11],10,-1120210379);c=ii(c,d,a,b,x[i+ 2],15,  718787259);
        b=ii(b,c,d,a,x[i+ 9],21, -343485551);a=ad(a,olda);b=ad(b,oldb);c=ad(c,oldc);d=ad(d,oldd);
    }
    return rh(a)+rh(b)+rh(c)+rh(d);
}
  }
}
</script>

<style lang="scss">
// .login-tabs-container {
//   min-height: max;

//   .con-tab {
//     padding-bottom: 14px;
//   }

//   .con-slot-tabs {
//     margin-top: 0rem;
//   }
// }
.login-background{
  min-height: 100%;
  background: linear-gradient(to right, #2e5984 0%, #0099cc 100%);
}

.theme-dark input {
  background: #ffffff;
  color: black;
}
</style>





