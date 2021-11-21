/*=========================================================================================
  File Name: router.js
  Description: Routes for vue-router. Lazy loading is enabled.
  Object Strucutre:
                    path => router path
                    name => router name
                    component(lazy loading) => component to load
                    meta : {
                      rule => which user can have access (ACL)
                      breadcrumb => Add breadcrumb to specific page
                      pageTitle => Display title besides breadcrumb
                    }
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


import Vue from 'vue'
import Router from 'vue-router'
// import auth from '@/auth/authService'

// import firebase from 'firebase/app'
// import 'firebase/auth'

Vue.use(Router)

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  scrollBehavior () {
    return { x: 0, y: 0 }
  },
  routes: [

    {
    // =============================================================================
    // MAIN LAYOUT ROUTES
    // =============================================================================
      path: '',
      component: () => import('./layouts/main/Main.vue'),
      children: [
        // =============================================================================
        // Theme Routes
        // =============================================================================
        {
          path: '/',
          redirect: 'pages/login'
        },
        // {
        //   path: '/dashboard/analytics',
        //   name: 'dashboard-analytics',
        //   component: () => import('./views/DashboardAnalytics.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/dashboard/ecommerce',
        //   name: 'dashboard-ecommerce',
        //   component: () => import('./views/DashboardECommerce.vue'),
        //   meta: {
        //     rule: 'Admin'
        //   }
        // },
        // =============================================================================
        // Application Routes
        // =============================================================================
        {
          path: '/apps/todo',
          redirect: '/apps/todo/all',
          name: 'todo'
        },
        {
          path: '/apps/todo/:filter',
          component: () => import('./views/apps/todo/Todo.vue'),
          meta: {
            rule: 'Editor',
            parent: 'todo',
            no_scroll: true
          }
        },
        {
          path: '/apps/chat',
          name: 'chat',
          component: () => import('./views/apps/chat/Chat.vue'),
          meta: {
            rule: 'Editor',
            no_scroll: true
          }
        },
        // {
        //   path: '/apps/email',
        //   redirect: '/apps/email/inbox',
        //   name: 'email'
        // },
        // {
        //   path: '/apps/email/:filter',
        //   component: () => import('./views/apps/email/Email.vue'),
        //   meta: {
        //     rule: 'Editor',
        //     parent: 'email',
        //     no_scroll: true
        //   }
        // },
        // {
        //   path: '/apps/calendar/vue-simple-calendar',
        //   name: 'calendar-simple-calendar',
        //   component: () => import('./views/apps/calendar/SimpleCalendar.vue'),
        //   meta: {
        //     rule: 'Editor',
        //     no_scroll: true
        //   }
        // },
        // {
        //   path: '/apps/dragAndDrop/dragAndDrop',
        //   name: 'dragAndDrop',
        //   component: () => import('./views/apps/dragAndDrop/dragAndDrop.vue'),
        //   meta: {
        //     rule: 'Editor',
        //     no_scroll: true
        //   }
        // },
        // {
        //   path: '/apps/myTeam/myTeam',
        //   name: 'myTeam',
        //   component: () => import('./views/apps/myTeam/myTeam.vue'),
        //   meta: {
        //     rule: 'Editor',
        //     no_scroll: true
        //   }
        // },
        {
          path: '/apps/activities/activities',
          name: 'activities',
          component: () => import('./views/apps/activities/activities.vue'),
          meta: {
            rule: 'Editor',
            no_scroll: true
          }
        },
        // {
        //   path: '/apps/eCommerce/shop',
        //   name: 'ecommerce-shop',
        //   component: () => import('./views/apps/eCommerce/ECommerceShop.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'eCommerce'},
        //       { title: 'Shop', active: true }
        //     ],
        //     pageTitle: 'Shop',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/apps/eCommerce/wish-list',
        //   name: 'ecommerce-wish-list',
        //   component: () => import('./views/apps/eCommerce/ECommerceWishList.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'eCommerce', url:'/apps/eCommerce/shop'},
        //       { title: 'Wish List', active: true }
        //     ],
        //     pageTitle: 'Wish List',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/apps/eCommerce/checkout',
        //   name: 'ecommerce-checkout',
        //   component: () => import('./views/apps/eCommerce/ECommerceCheckout.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'eCommerce', url:'/apps/eCommerce/shop'},
        //       { title: 'Checkout', active: true }
        //     ],
        //     pageTitle: 'Checkout',
        //     rule: 'Editor'
        //   }
        // },
        /*
                  Below route is for demo purpose
                  You can use this route in your app
                    {
                        path: '/apps/eCommerce/item/',
                        name: 'ecommerce-item-detail-view',
                        redirect: '/apps/eCommerce/shop',
                    }
                */
        // {
        //   path: '/apps/eCommerce/item/',
        //   redirect: '/apps/eCommerce/item/5546604'
        // },
        // {
        //   path: '/apps/eCommerce/item/:item_id',
        //   name: 'ecommerce-item-detail-view',
        //   component: () => import('./views/apps/eCommerce/ECommerceItemDetailView.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'eCommerce'},
        //       { title: 'Shop', url: {name: 'ecommerce-shop'} },
        //       { title: 'Item Details', active: true }
        //     ],
        //     parent: 'ecommerce-item-detail-view',
        //     pageTitle: 'Item Details',
        //     rule: 'Editor'
        //   }
        // },
        {
          path: '/apps/user/user-list',
          name: 'app-user-list',
          component: () => import('@/views/apps/user/user-list/UserList.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User' },
              { title: 'List', active: true }
            ],
            pageTitle: 'User List',
            rule: 'Editor'
          }
        },
        {
          path: '/apps/user/user-view/:userId',
          name: 'app-user-view',
          component: () => import('@/views/apps/user/UserView.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User' },
              { title: 'View', active: true }
            ],
            pageTitle: 'User View',
            rule: 'Editor'
          }
        },
        {
          path: '/apps/user/user-edit/:userId',
          name: 'app-user-edit',
          component: () => import('@/views/apps/user/user-edit/UserEdit.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'User' },
              { title: 'Edit', active: true }
            ],
            pageTitle: 'User Edit',
            rule: 'Editor'
          }
        },
        // =============================================================================
        // UI ELEMENTS
        // =============================================================================
        {
          path: '/ui-elements/data-list/list-view',
          name: 'data-list-list-view',
          component: () => import('@/views/ui-elements/data-list/list-view/DataListListView.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Data List'},
              { title: 'List View', active: true }
            ],
            pageTitle: 'List View',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/data-list/thumb-view',
          name: 'data-list-thumb-view',
          component: () => import('@/views/ui-elements/data-list/thumb-view/DataListThumbView.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Data List'},
              { title: 'Thumb View', active: true }
            ],
            pageTitle: 'Thumb View',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/grid/vuesax',
          name: 'grid-vuesax',
          component: () => import('@/views/ui-elements/grid/vuesax/GridVuesax.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Grid'},
              { title: 'Vuesax', active: true }
            ],
            pageTitle: 'Grid',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/grid/tailwind',
          name: 'grid-tailwind',
          component: () => import('@/views/ui-elements/grid/tailwind/GridTailwind.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Grid'},
              { title: 'Tailwind', active: true }
            ],
            pageTitle: 'Tailwind Grid',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/colors',
          name: 'colors',
          component: () => import('./views/ui-elements/colors/Colors.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Colors', active: true }
            ],
            pageTitle: 'Colors',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/card/basic',
          name: 'basic-cards',
          component: () => import('./views/ui-elements/card/CardBasic.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Card' },
              { title: 'Basic Cards', active: true }
            ],
            pageTitle: 'Basic Cards',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/card/statistics',
          name: 'statistics-cards',
          component: () => import('./views/ui-elements/card/CardStatistics.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Card' },
              { title: 'Statistics Cards', active: true }
            ],
            pageTitle: 'Statistics Card',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/card/analytics',
          name: 'analytics-cards',
          component: () => import('./views/ui-elements/card/CardAnalytics.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Card' },
              { title: 'Analytics Card', active: true }
            ],
            pageTitle: 'Analytics Card',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/card/card-actions',
          name: 'card-actions',
          component: () => import('./views/ui-elements/card/CardActions.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Card' },
              { title: 'Card Actions', active: true }
            ],
            pageTitle: 'Card Actions',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/card/card-colors',
          name: 'card-colors',
          component: () => import('./views/ui-elements/card/CardColors.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Card' },
              { title: 'Card Colors', active: true }
            ],
            pageTitle: 'Card Colors',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/table',
          name: 'table',
          component: () => import('./views/ui-elements/table/Table.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Table', active: true }
            ],
            pageTitle: 'Table',
            rule: 'Editor'
          }
        },
        {
          path: '/ui-elements/ag-grid-table',
          name: 'ag-grid-table',
          component: () => import('./views/ui-elements/ag-grid-table/AgGridTable.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'agGrid Table', active: true }
            ],
            pageTitle: 'agGrid Table',
            rule: 'Editor'
          }
        },

        // =============================================================================
        // COMPONENT ROUTES
        // =============================================================================
        {
          path: '/components/alert',
          name: 'component-alert',
          component: () => import('@/views/components/vuesax/alert/Alert.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Alert', active: true }
            ],
            pageTitle: 'Alert',
            rule: 'Editor'
          }
        },
        {
          path: '/components/avatar',
          name: 'component-avatar',
          component: () => import('@/views/components/vuesax/avatar/Avatar.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Avatar', active: true }
            ],
            pageTitle: 'Avatar',
            rule: 'Editor'
          }
        },
        {
          path: '/components/breadcrumb',
          name: 'component-breadcrumb',
          component: () => import('@/views/components/vuesax/breadcrumb/Breadcrumb.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Breadcrumb', active: true }
            ],
            pageTitle: 'Breadcrumb',
            rule: 'Editor'
          }
        },
        {
          path: '/components/button',
          name: 'component-button',
          component: () => import('@/views/components/vuesax/button/Button.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Button', active: true }
            ],
            pageTitle: 'Button',
            rule: 'Editor'
          }
        },
        {
          path: '/components/button-group',
          name: 'component-button-group',
          component: () => import('@/views/components/vuesax/button-group/ButtonGroup.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Button Group', active: true }
            ],
            pageTitle: 'Button Group',
            rule: 'Editor'
          }
        },
        {
          path: '/components/chip',
          name: 'component-chip',
          component: () => import('@/views/components/vuesax/chip/Chip.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Chip', active: true }
            ],
            pageTitle: 'Chip',
            rule: 'Editor'
          }
        },
        {
          path: '/components/collapse',
          name: 'component-collapse',
          component: () => import('@/views/components/vuesax/collapse/Collapse.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Collapse', active: true }
            ],
            pageTitle: 'Collapse',
            rule: 'Editor'
          }
        },
        {
          path: '/components/dialogs',
          name: 'component-dialog',
          component: () => import('@/views/components/vuesax/dialogs/Dialogs.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Dialogs', active: true }
            ],
            pageTitle: 'Dialogs',
            rule: 'Editor'
          }
        },
        {
          path: '/components/divider',
          name: 'component-divider',
          component: () => import('@/views/components/vuesax/divider/Divider.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Divider', active: true }
            ],
            pageTitle: 'Divider',
            rule: 'Editor'
          }
        },
        {
          path: '/components/dropdown',
          name: 'component-drop-down',
          component: () => import('@/views/components/vuesax/dropdown/Dropdown.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Dropdown', active: true }
            ],
            pageTitle: 'Dropdown',
            rule: 'Editor'
          }
        },
        {
          path: '/components/list',
          name: 'component-list',
          component: () => import('@/views/components/vuesax/list/List.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'List', active: true }
            ],
            pageTitle: 'List',
            rule: 'Editor'
          }
        },
        {
          path: '/components/loading',
          name: 'component-loading',
          component: () => import('@/views/components/vuesax/loading/Loading.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Loading', active: true }
            ],
            pageTitle: 'Loading',
            rule: 'Editor'
          }
        },
        {
          path: '/components/navbar',
          name: 'component-navbar',
          component: () => import('@/views/components/vuesax/navbar/Navbar.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Navbar', active: true }
            ],
            pageTitle: 'Navbar',
            rule: 'Editor'
          }
        },
        {
          path: '/components/notifications',
          name: 'component-notifications',
          component: () => import('@/views/components/vuesax/notifications/Notifications.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Notifications', active: true }
            ],
            pageTitle: 'Notifications',
            rule: 'Editor'
          }
        },
        {
          path: '/components/pagination',
          name: 'component-pagination',
          component: () => import('@/views/components/vuesax/pagination/Pagination.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Pagination', active: true }
            ],
            pageTitle: 'Pagination',
            rule: 'Editor'
          }
        },
        {
          path: '/components/popup',
          name: 'component-popup',
          component: () => import('@/views/components/vuesax/popup/Popup.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Popup', active: true }
            ],
            pageTitle: 'Popup',
            rule: 'Editor'
          }
        },
        {
          path: '/components/progress',
          name: 'component-progress',
          component: () => import('@/views/components/vuesax/progress/Progress.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Progress', active: true }
            ],
            pageTitle: 'Progress',
            rule: 'Editor'
          }
        },
        {
          path: '/components/sidebar',
          name: 'component-sidebar',
          component: () => import('@/views/components/vuesax/sidebar/Sidebar.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Sidebar', active: true }
            ],
            pageTitle: 'Sidebar',
            rule: 'Editor'
          }
        },
        {
          path: '/components/slider',
          name: 'component-slider',
          component: () => import('@/views/components/vuesax/slider/Slider.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Slider', active: true }
            ],
            pageTitle: 'Slider',
            rule: 'Editor'
          }
        },
        {
          path: '/components/tabs',
          name: 'component-tabs',
          component: () => import('@/views/components/vuesax/tabs/Tabs.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Tabs', active: true }
            ],
            pageTitle: 'Tabs',
            rule: 'Editor'
          }
        },
        {
          path: '/components/tooltip',
          name: 'component-tooltip',
          component: () => import('@/views/components/vuesax/tooltip/Tooltip.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Tooltip', active: true }
            ],
            pageTitle: 'Tooltip',
            rule: 'Editor'
          }
        },
        {
          path: '/components/upload',
          name: 'component-upload',
          component: () => import('@/views/components/vuesax/upload/Upload.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Components' },
              { title: 'Upload', active: true }
            ],
            pageTitle: 'Upload',
            rule: 'Editor'
          }
        },


        // =============================================================================
        // FORMS
        // =============================================================================
        // =============================================================================
        // FORM ELEMENTS
        // =============================================================================
        {
          path: '/forms/form-elements/select',
          name: 'form-element-select',
          component: () => import('./views/forms/form-elements/select/Select.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Select', active: true }
            ],
            pageTitle: 'Select',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/switch',
          name: 'form-element-switch',
          component: () => import('./views/forms/form-elements/switch/Switch.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Switch', active: true }
            ],
            pageTitle: 'Switch',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/checkbox',
          name: 'form-element-checkbox',
          component: () => import('./views/forms/form-elements/checkbox/Checkbox.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Checkbox', active: true }
            ],
            pageTitle: 'Checkbox',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/radio',
          name: 'form-element-radio',
          component: () => import('./views/forms/form-elements/radio/Radio.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Radio', active: true }
            ],
            pageTitle: 'Radio',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/input',
          name: 'form-element-input',
          component: () => import('./views/forms/form-elements/input/Input.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Input', active: true }
            ],
            pageTitle: 'Input',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/number-input',
          name: 'form-element-number-input',
          component: () => import('./views/forms/form-elements/number-input/NumberInput.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Number Input', active: true }
            ],
            pageTitle: 'Number Input',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-elements/textarea',
          name: 'form-element-textarea',
          component: () => import('./views/forms/form-elements/textarea/Textarea.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Form Elements' },
              { title: 'Textarea', active: true }
            ],
            pageTitle: 'Textarea',
            rule: 'Editor'
          }
        },
        // -------------------------------------------------------------------------------------------------------------------------------------------
        {
          path: '/forms/form-layouts',
          name: 'forms-form-layouts',
          component: () => import('@/views/forms/FormLayouts.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Forms' },
              { title: 'Form Layouts', active: true }
            ],
            pageTitle: 'Form Layouts',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-wizard',
          name: 'extra-component-form-wizard',
          component: () => import('@/views/forms/form-wizard/FormWizard.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Form Wizard', active: true }
            ],
            pageTitle: 'Form Wizard',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-validation',
          name: 'extra-component-form-validation',
          component: () => import('@/views/forms/form-validation/FormValidation.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Form Validation', active: true }
            ],
            pageTitle: 'Form Validation',
            rule: 'Editor'
          }
        },
        {
          path: '/forms/form-input-group',
          name: 'extra-component-form-input-group',
          component: () => import('@/views/forms/form-input-group/FormInputGroup.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Form Input Group', active: true }
            ],
            pageTitle: 'Form Input Group',
            rule: 'Editor'
          }
        },
        // =============================================================================
        // Pages Routes
        // =============================================================================
        {
          path: '/pages/home',
          name: 'page-home',
          component: () => import('@/views/pages/Home.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/profile',
          name: 'page-profile',
          component: () => import('@/views/pages/Profile.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/newPost',
          name: 'new-post',
          component: () => import('@/views/pages/NewPost.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/viewPhotos',
          name: 'view-photos',
          component: () => import('@/views/pages/ViewPhotos.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/friendList',
          name: 'friend-list',
          component: () => import('@/views/pages/FriendList.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        
        // {
        //   path: '/pages/profile',
        //   name: 'page-profile',
        //   component: () => import('@/views/pages/Profile.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        {
          path: '/pages/user-settings',
          name: 'page-user-settings',
          component: () => import('@/views/pages/user-settings/UserSettings.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Pages' },
              { title: 'User Settings', active: true }
            ],
            pageTitle: 'Settings',
            rule: 'Editor'
          }
        },
        // {
        //   path: '/pages/faq',
        //   name: 'page-faq',
        //   component: () => import('@/views/pages/Faq.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Pages' },
        //       { title: 'FAQ', active: true }
        //     ],
        //     pageTitle: 'FAQ',
        //     rule: 'Editor'
        //   }
        // },
        {
          path: '/pages/knowledge-base',
          name: 'page-knowledge-base',
          component: () => import('@/views/pages/KnowledgeBase.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Pages' },
              { title: 'KnowledgeBase', active: true }
            ],
            pageTitle: 'KnowledgeBase',
            rule: 'Editor'
          }
        },
        // {
        //   path: '/pages/knowledge-base/category',
        //   name: 'page-knowledge-base-category',
        //   component: () => import('@/views/pages/KnowledgeBaseCategory.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Pages' },
        //       { title: 'KnowledgeBase', url: '/pages/knowledge-base' },
        //       { title: 'Category', active: true }
        //     ],
        //     parent: 'page-knowledge-base',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/pages/knowledge-base/category/question',
        //   name: 'page-knowledge-base-category-question',
        //   component: () => import('@/views/pages/KnowledgeBaseCategoryQuestion.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Pages' },
        //       { title: 'KnowledgeBase', url: '/pages/knowledge-base' },
        //       { title: 'Category', url: '/pages/knowledge-base/category' },
        //       { title: 'Question', active: true }
        //     ],
        //     parent: 'page-knowledge-base',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/pages/search',
        //   name: 'page-search',
        //   component: () => import('@/views/pages/Search.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Pages' },
        //       { title: 'Search', active: true }
        //     ],
        //     pageTitle: 'Search',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/pages/invoice',
        //   name: 'page-invoice',
        //   component: () => import('@/views/pages/Invoice.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Pages' },
        //       { title: 'Invoice', active: true }
        //     ],
        //     pageTitle: 'Invoice',
        //     rule: 'Editor'
        //   }
        // },

        // =============================================================================
        // CHARTS & MAPS
        // =============================================================================
        // {
        //   path: '/charts-and-maps/charts/apex-charts',
        //   name: 'extra-component-charts-apex-charts',
        //   component: () => import('@/views/charts-and-maps/charts/apex-charts/ApexCharts.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Charts & Maps' },
        //       { title: 'Apex Charts', active: true }
        //     ],
        //     pageTitle: 'Apex Charts',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/charts-and-maps/charts/chartjs',
        //   name: 'extra-component-charts-chartjs',
        //   component: () => import('@/views/charts-and-maps/charts/chartjs/Chartjs.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Charts & Maps' },
        //       { title: 'chartjs', active: true }
        //     ],
        //     pageTitle: 'chartjs',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/charts-and-maps/charts/echarts',
        //   name: 'extra-component-charts-echarts',
        //   component: () => import('@/views/charts-and-maps/charts/echarts/Echarts.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Charts & Maps' },
        //       { title: 'echarts', active: true }
        //     ],
        //     pageTitle: 'echarts',
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/charts-and-maps/maps/google-map',
        //   name: 'extra-component-maps-google-map',
        //   component: () => import('@/views/charts-and-maps/maps/google-map/GoogleMap.vue'),
        //   meta: {
        //     breadcrumb: [
        //       { title: 'Home', url: '/' },
        //       { title: 'Charts & Maps' },
        //       { title: 'Google Map', active: true }
        //     ],
        //     pageTitle: 'Google Map',
        //     rule: 'Editor'
        //   }
        // },


        // =============================================================================
        // EXTENSIONS
        // =============================================================================
        {
          path: '/extensions/select',
          name: 'extra-component-select',
          component: () => import('@/views/components/extra-components/select/Select.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Select', active: true }
            ],
            pageTitle: 'Select',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/quill-editor',
          name: 'extra-component-quill-editor',
          component: () => import('@/views/components/extra-components/quill-editor/QuillEditor.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Quill Editor', active: true }
            ],
            pageTitle: 'Quill Editor',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/drag-and-drop',
          name: 'extra-component-drag-and-drop',
          component: () => import('@/views/components/extra-components/drag-and-drop/DragAndDrop.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Drag & Drop', active: true }
            ],
            pageTitle: 'Drag & Drop',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/datepicker',
          name: 'extra-component-datepicker',
          component: () => import('@/views/components/extra-components/datepicker/Datepicker.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Datepicker', active: true }
            ],
            pageTitle: 'Datepicker',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/datetime-picker',
          name: 'extra-component-datetime-picker',
          component: () => import('@/views/components/extra-components/datetime-picker/DatetimePicker.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extra Components' },
              { title: 'Datetime Picker', active: true }
            ],
            pageTitle: 'Datetime Picker',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/access-control',
          name: 'extra-component-access-control',
          component: () => import('@/views/components/extra-components/access-control/AccessControl.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Access Control', active: true }
            ],
            pageTitle: 'Access Control',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/i18n',
          name: 'extra-component-i18n',
          component: () => import('@/views/components/extra-components/I18n.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'I18n', active: true }
            ],
            pageTitle: 'I18n',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/carousel',
          name: 'extra-component-carousel',
          component: () => import('@/views/components/extra-components/carousel/Carousel.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Carousel', active: true }
            ],
            pageTitle: 'Carousel',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/clipboard',
          name: 'extra-component-clipboard',
          component: () => import('@/views/components/extra-components/clipboard/Clipboard.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Clipboard', active: true }
            ],
            pageTitle: 'Clipboard',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/context-menu',
          name: 'extra-component-context-menu',
          component: () => import('@/views/components/extra-components/context-menu/ContextMenu.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Context Menu', active: true }
            ],
            pageTitle: 'Context Menu',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/star-ratings',
          name: 'extra-component-star-ratings',
          component: () => import('@/views/components/extra-components/star-ratings/StarRatings.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Star Ratings', active: true }
            ],
            pageTitle: 'Star Ratings',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/autocomplete',
          name: 'extra-component-autocomplete',
          component: () => import('@/views/components/extra-components/autocomplete/Autocomplete.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Autocomplete', active: true }
            ],
            pageTitle: 'Autocomplete',
            rule: 'Editor'
          }
        },
        {
          path: '/extensions/tree',
          name: 'extra-component-tree',
          component: () => import('@/views/components/extra-components/tree/Tree.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Tree', active: true }
            ],
            pageTitle: 'Tree',
            rule: 'Editor'
          }
        },
        {
          path: '/import-export/import',
          name: 'import-excel',
          component: () => import('@/views/components/extra-components/import-export/Import.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Import/Export' },
              { title: 'Import', active: true }
            ],
            pageTitle: 'Import Excel',
            rule: 'Editor'
          }
        },
        {
          path: '/import-export/export',
          name: 'export-excel',
          component: () => import('@/views/components/extra-components/import-export/Export.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Import/Export' },
              { title: 'Export', active: true }
            ],
            pageTitle: 'Export Excel',
            rule: 'Editor'
          }
        },
        {
          path: '/import-export/export-selected',
          name: 'export-excel-selected',
          component: () => import('@/views/components/extra-components/import-export/ExportSelected.vue'),
          meta: {
            breadcrumb: [
              { title: 'Home', url: '/' },
              { title: 'Extensions' },
              { title: 'Import/Export' },
              { title: 'Export Selected', active: true }
            ],
            pageTitle: 'Export Excel',
            rule: 'Editor'
          }
        }
      ]
    },
    // =============================================================================
    // FULL PAGE LAYOUTS
    // =============================================================================
    {
      path: '',
      component: () => import('@/layouts/full-page/FullPage.vue'),
      children: [
        // =============================================================================
        // PAGES
        // =============================================================================
        // {
        //   path: '/callback',
        //   name: 'auth-callback',
        //   component: () => import('@/views/Callback.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        
        {
          path: '/pages/login',
          name: 'page-login',
          component: () => import('@/views/pages/login/Login.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/register',
          name: 'page-register',
          component: () => import('@/views/pages/register/Register.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/forgot-password',
          name: 'page-forgot-password',
          component: () => import('@/views/pages/ForgotPassword.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        {
          path: '/pages/reset-password',
          name: 'page-reset-password',
          component: () => import('@/views/pages/ResetPassword.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        // {
        //   path: '/pages/lock-screen',
        //   name: 'page-lock-screen',
        //   component: () => import('@/views/pages/LockScreen.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        // {
        //   path: '/pages/comingsoon',
        //   name: 'page-coming-soon',
        //   component: () => import('@/views/pages/ComingSoon.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        {
          path: '/pages/error-404',
          name: 'page-error-404',
          component: () => import('@/views/pages/Error404.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        // {
        //   path: '/pages/error-500',
        //   name: 'page-error-500',
        //   component: () => import('@/views/pages/Error500.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // },
        {
          path: '/pages/not-authorized',
          name: 'page-not-authorized',
          component: () => import('@/views/pages/NotAuthorized.vue'),
          meta: {
            rule: 'Editor'
          }
        },
        // {
        //   path: '/pages/maintenance',
        //   name: 'page-maintenance',
        //   component: () => import('@/views/pages/Maintenance.vue'),
        //   meta: {
        //     rule: 'Editor'
        //   }
        // }
      ]
    },
    {
      path: '/pages/login',
      redirect: '/login'
    },
    // Redirect to 404 page, if no match found
    {
      path: '*',
      redirect: '/pages/error-404'
    }
  ]
})

router.afterEach(() => {
  // Remove initial loading
  const appLoading = document.getElementById('loading-bg')
  if (appLoading) {
    appLoading.style.display = 'none'
  }
})

router.beforeEach((to, from, next) => {
  return next();
  //firebase.auth().onAuthStateChanged(() => {
    

    // get firebase current user
    //const firebaseCurrentUser = firebase.auth().currentUser

    // if (
    //     to.path === "/pages/login" ||
    //     to.path === "/pages/forgot-password" ||
    //     to.path === "/pages/error-404" ||
    //     to.path === "/pages/error-500" ||
    //     to.path === "/pages/register" ||
    //     to.path === "/callback" ||
    //     to.path === "/pages/comingsoon" ||
    //     (auth.isAuthenticated() || firebaseCurrentUser)
    // ) {
    //     return next();
    // }

    // If auth required, check login. If login fails redirect to login page
    // if (to.meta.authRequired) {
    //   if (!(auth.isAuthenticated() || firebaseCurrentUser)) {
    //     router.push({ path: '/pages/login', query: { to: to.path } })
    //   }
    // }

    
    // Specify the current path as the customState parameter, meaning it
    // will be returned to the application after auth
    // auth.login({ target: to.path });

  //})

})

export default router