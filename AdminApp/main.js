import Vue from 'vue'
import App from './App'
import Vuetify from 'vuetify'
import router from './router'
import 'font-awesome/css/font-awesome.css'
import './theme/default.styl'
import VeeValidate from 'vee-validate'
import Truncate from 'lodash.truncate'

Vue.config.productionTip = false
// Helpers
// Global filters
Vue.filter('truncate', Truncate)
Vue.use(VeeValidate, { fieldsBagName: 'formFields' })
Vue.use(Vuetify, {
  options: {
    themeVariations: ['primary', 'secondary', 'accent'],
    extra: {
      mainToolbar: {
        color: 'primary'
      },
      sideToolbar: {
      },
      sideNav: 'primary',
      mainNav: 'primary lighten-1',
      bodyBg: ''
    }
  }
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
