'use strict'
import Util from '@/util'
export default {
  install(Vue, options) {
    const _localStorage = window.localStorage

    Vue.prototype.$myLocalStorage = {
      getEnc(key) {
        return Util.decrypt(_localStorage.getItem(key))
      },
      setEnc(key, data) {
        _localStorage.setItem(key, Util.encrypt(data))
      }
    }

    window.$myLocalStorage = Vue.prototype.$myLocalStorage
  }
}
