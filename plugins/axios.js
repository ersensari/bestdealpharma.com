'use strict'
import axios from 'axios'
import NProgress from 'nprogress'
import '@/theme/nprogress.css'

export default {
  install (Vue, options) {
    let config = {}
    const _axios = axios.create(config)

    _axios.interceptors.request.use(
      config => {
        if (options && options.showSpinner) NProgress.configure({ showSpinner: true })
        if (options && options.useProgress) NProgress.start()

        if (window.localStorage.getItem('token')) {
          config.headers['Authorization'] = 'Bearer ' + window.localStorage.getItem('token')
        }
        return config
      },
      error => {
        NProgress.done()
        return Promise.reject(error)
      }
    )

    // Add a response interceptor
    _axios.interceptors.response.use(
      function (response) {
        NProgress.done()
        return response
      },
      function (error) {
        NProgress.done()
        return Promise.reject(error)
      }
    )

    Vue.axios = _axios
    window.axios = _axios
    Object.defineProperties(Vue.prototype, {
      axios: {
        get () {
          return _axios
        }
      },
      $axios: {
        get () {
          return _axios
        }
      }
    })
  }
}
