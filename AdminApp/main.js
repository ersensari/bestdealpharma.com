/* eslint-disable no-undef */
import Vue from 'vue'
import App from './App'
import { sync } from 'vuex-router-sync'

import Vuetify from 'vuetify'
import router from './router'
import 'font-awesome/css/font-awesome.css'
import './theme/default.styl'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

import VeeValidate from 'vee-validate'
import Truncate from 'lodash.truncate'
import axios from 'plugins/axios'
import myLocalStorage from 'plugins/myLocalStorage'
import store from '@store'

// import colors from 'vuetify/es5/util/colors'

Vue.config.productionTip = false

// Helpers
// Global filters
Vue.filter('truncate', Truncate)
Vue.use(VeeValidate, { fieldsBagName: 'formFields' })
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
})

Vue.use(axios)
Vue.use(myLocalStorage)
sync(store, router)

/* eslint-disable no-new */
const app = new Vue({
  router,
  store,
  ...App
})

export { app, router }
