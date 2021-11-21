<!-- =========================================================================================
  File Name: DashboardAnalytics.vue
  Description: Dashboard Analytics
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
========================================================================================== -->

<template>
  <div id="dashboard-analytics">

    <div class="vx-row">
      <div class="vx-col w-full">
        <vx-card title="My Team">
          <div slot="no-body" class="mt-4">
            <vs-table :data="dispatchedOrders" class="table-dark-inverted">
              <template slot="thead">
                <!-- <vs-th>STATUS</vs-th> -->
                <vs-th>NAME AND SURNAME</vs-th>
                <vs-th>EMAIL</vs-th>
                <!-- <vs-th>BUSY WITH</vs-th>
                <vs-th>LAST ONLINE</vs-th> -->
              </template>

              <template slot-scope="{data}">
                <vs-tr :key="indextr" v-for="(tr, indextr) in data">
                  <!-- <vs-td :data="data[indextr].status">
                    <span class="flex items-center px-2 py-1 rounded"><div class="h-3 w-3 rounded-full mr-2" :class="'bg-' + data[indextr].statusColor"></div>{{data[indextr].status}}</span>
                  </vs-td> -->
                  <vs-td :data="data[indextr].orderNo">
                    <vs-avatar :src="data[indextr].operatorImg" size="30px" class="border-2 border-white border-solid -m-1"></vs-avatar>
                    <span class="pl-2 pb-2">{{data[indextr].operator}}</span>
                  </vs-td>
                  <vs-td :data="data[indextr].orderNo">
                    <span>{{data[indextr].email}}</span>
                  </vs-td>
                  <!-- <vs-td :data="data[indextr].orderNo">
                    <span>{{data[indextr].location}}</span>
                  </vs-td>
                  <vs-td :data="data[indextr].orderNo">
                    <span>{{data[indextr].startDate}}</span>
                  </vs-td> -->
                </vs-tr>
              </template>
            </vs-table>
          </div>
        </vx-card>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  data () {
    return {
      dispatchedOrders: []
    }
  },
  components: {
  },
  methods: {
    getUserInfo(){
      var self = this;

      var onSuccess = function (response) {
        
      };
      self.$ajaxGet(self, "Item/getItemList", onSuccess);
    }
  },
  created () {
    var accesstoken = localStorage.getItem("accessToken");

    if(!accesstoken){
      this.$router.push('/pages/login');
    }
    
    var self = this;
      // Friends list
    this.$http.get('/api/table/dispatched-orders')
      .then((response) => { this.dispatchedOrders = response.data })
      .catch((error)   => { console.log(error) })
  },
  mounted(){
    var self = this;
    self.getUserInfo();
  }
}
</script>

<style lang="scss">
/*! rtl:begin:ignore */
#dashboard-analytics {
  .greet-user{
    position: relative;

    .decore-left{
      position: absolute;
      left:0;
      top: 0;
    }
    .decore-right{
      position: absolute;
      right:0;
      top: 0;
    }
  }

  @media(max-width: 576px) {
    .decore-left, .decore-right{
      width: 140px;
    }
  }
}
/*! rtl:end:ignore */
</style>
