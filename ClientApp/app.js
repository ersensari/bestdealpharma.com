import Vue from 'vue'
import router from './router/index'
import axiosPlugin from 'plugins/axios'
import myLocalStorage from 'plugins/myLocalStorage'
import myUtil from 'plugins/myUtil'
import store from '@store'

import {sync} from 'vuex-router-sync'
import App from './app-root'
import {FontAwesomeIcon} from './icons'
import Vuetify from 'vuetify'

import 'vuetify/dist/vuetify.min.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

import VuejsDialog from 'vuejs-dialog'
import VuetifyConfirm from 'vuetify-confirm'
import VueToastr from '@deveodk/vue-toastr'
import '@deveodk/vue-toastr/dist/@deveodk/vue-toastr.css'

import VueCookies from 'vue-cookies'

import lodash from 'lodash'

Vue.use(VueCookies);

VueCookies.config('7d');

Vue.use(myUtil);

Vue.use(Vuetify, {
  theme: {
    primary: '#29B6F6',
    secondary: '#0277BD',
    accent: '#206731',
    error: '#E65100',
    warning: '#FFE57F',
    info: '#80D8FF',
    success: '#00E676'
  }
});

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

Vue.component('fa-icon', FontAwesomeIcon);
Vue.use(axiosPlugin, {showSpinner: false, useProgress: true});

Vue.use(myLocalStorage);

Vue.filter('capitalize', (value) => {
  return value.toString().toUpperCase()
});

Vue.filter('currency', (value) => {
  let val = (value / 1).toFixed(2).replace('.', ',');
  return '$' + val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
});

sync(store, router);

const app = new Vue({
  store,
  router,
  ...App
});

export {app, router, store}

