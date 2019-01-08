'use strict'
import Util from '@/util'

export default {
  install(Vue, options) {
    Vue.prototype.$myUtil = Util
    window.$myUtil = Util

  }
}
