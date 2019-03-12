/* eslint-disable no-undef */
import Vue from 'vue'
import App from './App'
import {sync} from 'vuex-router-sync'

import Vuetify from 'vuetify'
import router from './router'
import 'font-awesome/css/font-awesome.css'
import './theme/default.styl'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

import Truncate from 'lodash.truncate'
import axiosPlugin from 'plugins/axios'
import myLocalStorage from 'plugins/myLocalStorage'
import store from '@store'

import VuejsDialog from 'vuejs-dialog'
import VuetifyConfirm from 'vuetify-confirm'
import VueToastr from '@deveodk/vue-toastr'
import '@deveodk/vue-toastr/dist/@deveodk/vue-toastr.css'

// import colors from 'vuetify/es5/util/colors'

import GlobalProperties from 'plugins/globalProperties'
import moment from 'moment'
import lodash from 'lodash'


Vue.use(GlobalProperties);

Vue.use(VuejsDialog, {
  html: true,
  loader: true,
  okText: 'Ok',
  cancelText: 'Cancal',
  animation: 'bounce'
});

Vue.use(VuetifyConfirm, {
  buttonTrueText: 'OK',
  buttonFalseText: 'CANCEL',
  color: 'warning',
  icon: 'warning',
  title: 'Warning',
  width: 300,
  property: '$confirm'
});

Vue.use(VueToastr, {
  defaultPosition: 'toast-bottom-right',
  defaultType: 'info',
  defaultTimeout: 3000
});

Vue.config.productionTip = false;

// Helpers
// Global filters
Vue.filter('truncate', Truncate);

Vue.use(Vuetify, {
  options: {
    // themeVariations: ['primary', 'secondary', 'accent'],
    // extra: {
    //   mainToolbar: {
    //     color: 'primary'
    //   },
    //   sideToolbar: {
    //   },
    //   sideNav: 'primary',
    //   mainNav: 'primary lighten-1',
    //   bodyBg: ''
    // },
    theme: {
      primary: '#3f51b5',
      secondary: '#b0bec5',
      accent: '#8c9eff',
      error: '#b71c1c'
    }
  }
});

Vue.filter('currency', (value) => {
  let val = (value / 1).toFixed(2).replace('.', ',');
  return '$' + val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
});

Vue.filter('formatdate', (value) => {
  return moment(value).format('LL')
});

Vue.filter('formatStatus', (value) => {
  switch (value) {
    case 0:
      return 'Waiting Payment';
    case 1:
      return 'Preparing';
    case 2:
      return 'Shipped';
    case 3:
      return 'Canceled';

  }
});
Vue.use(axiosPlugin, {showSpinner: true, useProgress: true});
Vue.use(myLocalStorage);
sync(store, router);

/* eslint-disable no-new */
const app = new Vue({
  router,
  store,
  ...App
});

export {app, router}
