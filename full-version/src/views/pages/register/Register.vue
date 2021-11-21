<template>
    <div class="h-screen flex w-full  vx-row no-gutter items-center justify-center" style="background: linear-gradient(to right, #2e5984 0%, #0099cc 100%)">
        <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-3/5 sm:m-0 m-4">
          <div slot="no-body">
              <div class="vx-row no-gutter">
                  <div class="vx-col sm:w-full md:w-full lg:w-1/2 mx-auto self-center pb-4" style="background-color: #00224B">
                      <div class="px-8 pt-6 register-tabs-container">
                          <div class="vx-card__title mb-4">
                            <h2 style="color: white; display: flex; justify-content: center;" class="mb-4"><strong>Create Account</strong></h2>
                          </div>
                          <div>
                            <label style="color: white"><strong>Name</strong></label>
                          </div>
                            <vs-input
                              v-validate="'required'"
                              data-vv-validate-on="blur"
                              name="name"
                              autocomplete="off"
                              placeholder="Name"
                              v-model="name"
                              class="w-full" />
                            <span class="text-danger text-sm">{{ errors.first('name') }}</span>
                            <div>
                              <label class="mt-6" style="color: white"><strong>Surname</strong></label>
                            </div>
                            <vs-input
                              v-validate="'required'"
                              data-vv-validate-on="blur"
                              name="surname"
                              autocomplete="off"
                              placeholder="Surname"
                              v-model="surname"
                              class="w-full"/>
                            <span class="text-danger text-sm">{{ errors.first('surname') }}</span>

                            <div>
                            <label class="mt-6" style="color: white"><strong>Mobile Number</strong></label>
                            </div>
                            <vs-input
                              v-validate="'required|integer|min:10|max:10'"
                              type="text"
                              data-vv-validate-on="blur"
                              name="Mobile Number"
                              autocomplete="off"
                              placeholder="Mobile Number"
                              v-model="mobileNumber"
                              class="w-full" />
                            <span class="text-danger text-sm">{{ errors.first('Mobile Number') }}</span>

                            <div>
                              <label class="mt-6" style="color: white"><strong>Email</strong></label>
                            </div>
                            <vs-input
                              v-validate="'required|email'"
                              data-vv-validate-on="blur"
                              name="email"
                              autocomplete="off"
                              type="email"
                              placeholder="Email"
                              v-model="email"
                              class="w-full" />
                            <span class="text-danger text-sm">{{ errors.first('email') }}</span>
                            <div>
                              <label class="mt-6" style="color: white"><strong>Password</strong></label>
                            </div>
                            <vs-input
                              ref="password"
                              type="password"
                              data-vv-validate-on="blur"
                              v-validate="'required|min:5'"
                              name="password"
                              autocomplete="off"
                              placeholder="Password"
                              v-model="password"
                              class="w-full" />
                            <span class="text-danger text-sm">{{ errors.first('password') }}</span>
                            <div v-show="password">
                              <label class="mt-6" style="color: white"><strong>Confirm Password</strong></label>
                            </div>
                            <vs-input
                              v-show="password"
                              type="password"
                              v-validate="'min:5|confirmed:password'"
                              data-vv-validate-on="blur"
                              data-vv-as="password"
                              name="confirm_password"
                              autocomplete="off"
                              placeholder="Confirm Password"
                              v-model="confirm_password"
                              class="w-full" />
                            <span class="text-danger text-sm">{{ errors.first('confirm_password') }}</span>

                            <!-- <vs-checkbox v-model="isTermsConditionAccepted" class="mt-6">I accept the terms & conditions.</vs-checkbox> -->
                            <!-- <vs-button  type="border" to="/pages/login" class="mt-6">Login</vs-button> -->
                            <div style="display: flex; justify-content: center;">
                              <vs-button class=" mt-6" style="background-color: #2E5984 !important; color: white;" v-ripple.400="'rgba(255, 255, 255, 0.15)'" @click="registerUserJWt" :disabled="!validateForm">Register</vs-button>
                            </div>
                          <!-- <vs-tabs>
                            <vs-tab label="JWT">
                              <register-jwt></register-jwt>
                            </vs-tab>

                            <vs-tab label="Firebase">
                              <register-firebase></register-firebase>
                            </vs-tab>

                            <vs-tab label="Auth0">
                              <register-auth0></register-auth0>
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
import RegisterJwt from './RegisterJWT.vue'
import RegisterFirebase from './RegisterFirebase.vue'
import RegisterAuth0 from './RegisterAuth0.vue'

export default {
  components: {
    RegisterJwt,
    RegisterFirebase,
    RegisterAuth0
  },
  data () {
    return {
      name: '',
      surname: '',
      mobileNumber: '',
      email: "",
      password: '',
      confirm_password: '',
      isTermsConditionAccepted: true
    }
  },
  computed: {
    validateForm () {
      return !this.errors.any() && this.name !== '' && this.surname !== '' && this.email !== '' && this.password !== '' && this.confirm_password !== '' && this.mobileNumber !=='';
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

        return false
      }
      return true
    },
    registerUserJWt () {
      var self = this;
      // If form is not validated or user is already login return
      if (!this.validateForm || !this.checkLogin()) return

        var userDetails = {
          name: self.name,
          surname: self.surname,
          email: self.email,
          password: self.md5(self.password),
          mobileNumber: self.mobileNumber
        }


      var onSuccess = function (response) {
        self.$vs.notify({
          title:'Information',
          color: "success",
          text: "You have registered",
          position:'top-right',
          iconPack: 'feather',
          icon:'icon-mail'
        });

        self.$router.push("/pages/login");
      };

      self.$ajaxPostAnon(self, "User/registerUser", userDetails, onSuccess);
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
.register-tabs-container {
  min-height: 517px;

  .con-tab {
    padding-bottom: 23px;
  }
}

.theme-dark input {
  background: #ffffff !important;
  color: black !important;
}
</style>
