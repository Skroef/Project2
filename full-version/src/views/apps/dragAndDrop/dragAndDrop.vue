<!-- =========================================================================================
    File Name: DragAndDropMultipleLists.vue
    Description: Drag and Drop to multiple lists
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
      Author: Pixinvent
    Author URL: http://www.themeforest.net/user/pixinvent
========================================================================================== -->


<template>
    <vx-card title="Board" code-toggler>

      <div style="width: 130px" class="btn-add-new p-3 mb-4 mr-4 rounded-lg cursor-pointer flex items-center justify-center text-lg font-medium text-base text-primary border border-solid border-primary" @click="activePromptAddItem = true">
          <feather-icon icon="PlusIcon" svgClasses="h-4 w-4" />
          <span class="ml-2 text-base text-primary">Add New</span>
      </div>

        <p>Drag and drop items from one list to the other</p>

        <!-- List 1 -->
        <div class="vx-row">
            <div class="vx-col w-full md:w-1/3">
                <vs-list>
                    <vs-list-header title="To Do:" color="primary"></vs-list-header>
                    <draggable :list="toDoList" group="people" class="p-2 cursor-move">

                        <vs-list-item v-for="(listItem, index) in toDoList" :key="index" 
                          :title="listItem.topic" 
                          :subtitle="listItem.toDo"
                        ><vs-avatar slot="avatar" :text="listItem.topic" /></vs-list-item>

                    </draggable>
                </vs-list>
            </div>
            <div class="vx-col w-full md:w-1/3">
                <vs-list>
                    <vs-list-header title="Doing" color="primary"></vs-list-header>
                    <draggable :list="doingList" group="people" class="p-2 cursor-move">

                      <vs-list-item v-for="(listItem, index) in doingList" :key="index" 
                        :title="listItem.topic" 
                        :subtitle="listItem.toDo"
                      ><vs-avatar slot="avatar" :text="listItem.topic" /></vs-list-item>

                    </draggable>
                </vs-list>
            </div>
            <div class="vx-col w-full md:w-1/3">
                <vs-list>
                    <vs-list-header title="Done" color="primary"></vs-list-header>
                    <draggable :list="doneList" group="people" class="p-2 cursor-move">

                      <vs-list-item v-for="(listItem, index) in doneList" :key="index" 
                        :title="listItem.topic" 
                        :subtitle="listItem.toDo"
                      ><vs-avatar slot="avatar" :text="listItem.topic" /></vs-list-item>

                    </draggable>
                </vs-list>
            </div>

            
          <vs-prompt
                class="calendar-event-dialog"
                title="Add Item"
                accept-text= "Add Item"
                @accept="addNewItem"
                :is-valid="validForm"
                :active.sync="activePromptAddItem">

                <div class="calendar__label-container">

                  <b-row>
                    <vs-input
                      name="Topic"
                      icon-no-border
                      icon="icon icon-list"
                      icon-pack="feather"
                      label-placeholder="Topic"
                      v-model="newItem.topic"
                      class="w-full p-3"/>
                  </b-row>


                  <label for="textarea-default">Details</label>

                  <b-row class="p-3">
                    <b-form-textarea
                      id="textarea-default"
                      placeholder="Enter Detail Here"
                      v-model="newItem.toDo"
                      rows="3"
                    />
                  </b-row>

                </div>

            </vs-prompt>

        </div>
    </vx-card>

</template>

<script>
import draggable from 'vuedraggable'
import Prism from 'vue-prism-component'

export default {
  data () {
    return {
      toDoList: [],
      doingList: [],
      doneList: [],
      newItem: {
        topic: '',
        toDo: ''
      },
      activePromptAddItem: false
    }
  },
  components: {
    draggable,
    Prism
  },
  computed:{
    validForm () {
      return this.newItem.topic !== '' && this.newItem.toDo !== ''
    },
  },
  methods:{
    getLists(){
      var self = this;

      var onSuccess = response =>{
        self.toDoList = response.data.toDo;
        self.doingList = response.data.doing;
        self.doneList = response.data.done;

        self.activePromptAddItem = false;
      }
    
      self.$ajaxGet(self, "DragAndDrop/GetAllDragAndDropItems", onSuccess);
    },
    addNewItem(){
      var self = this;

      var onSuccess = response =>{
        self.getLists();
      }
      var onFinally = response =>{
        this.$vs.notify({
          title: 'Information',
          text: 'Item Added',
          iconPack: 'feather',
          icon: 'icon-alert-circle',
          color: 'success'
        })
        self.newItem.topic = '';
        self.newItem.toDo = '';
      }


      self.$ajaxPost(self, "DragAndDrop/GetAllDragAndDropItems", self.newItem ,onSuccess, onFinally);
    }
  },
  mounted(){
    var self = this;

    self.getLists();
  },

  

}
</script>
