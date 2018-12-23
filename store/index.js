import Vue from 'vue'
import Vuex from 'vuex'
import user from './modules/user'
import links from './modules/links'
import pages from './modules/pages'
import snackbar from './modules/snackbar'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    user,
    links,
    pages,
    snackbar
  },
  strict: false
})
