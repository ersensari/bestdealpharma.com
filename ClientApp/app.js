import Vue from 'vue'
import router from './router/index'
import axiosPlugin from 'plugins/axios'
import myLocalStorage from 'plugins/myLocalStorage'
import store from '@store'

import {sync} from 'vuex-router-sync'
import App from 'components/app-root'
import {FontAwesomeIcon} from './icons'
import Vuetify from 'vuetify'

import 'vuetify/dist/vuetify.min.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

Vue.use(Vuetify, {
  theme: {
    primary: '#29B6F6',
    secondary: '#0277BD',
    accent: '#00E676',
    error: '#E65100',
    warning: '#FFE57F',
    info: '#80D8FF',
    success: '#00E676'
  }
});

// Registration of global components
Vue.component('icon', FontAwesomeIcon);

Vue.use(axiosPlugin, {showSpinner: false, useProgress: true});

Vue.use(myLocalStorage);

Vue.filter('capitalize', (value) => {
  return value.toString().toUpperCase()
});

sync(store, router);

const app = new Vue({
  store,
  router,
  ...App
});

export {app, router, store}

