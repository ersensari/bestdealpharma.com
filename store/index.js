import Vue from 'vue'
import Vuex from 'vuex'
import user from './modules/user'
import links from './modules/links'
import pages from './modules/pages'
import products from './modules/products'
import members from './modules/members'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    user,
    links,
    pages,
    products,
    members
  },
  strict: false
})
