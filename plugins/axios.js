'use strict'
import axios from 'axios'
export default {
  install(Vue) {
    // Full config:  https://github.com/axios/axios#request-config
    // axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
    // axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
    // axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';
    let config = {
      // baseURL: process.env.baseURL || process.env.apiUrl || ""
      // timeout: 60 * 1000, // Timeout
      // withCredentials: true, // Check cross-site Access-Control
    }

    const _axios = axios.create(config)

    _axios.interceptors.request.use((config) => {
      if (window.localStorage.getItem('token')) {
        config.headers['Authorization'] = 'Bearer ' + window.localStorage.getItem('token')
      }

      return config
    },
      (error) => {
        // Do something with request error
        return Promise.reject(error)
      }
    )

    // Add a response interceptor
    _axios.interceptors.response.use(
      function (response) {
        // Do something with response data
        return response
      },
      function (error) {
        // Do something with response error
        return Promise.reject(error)
      }
    )

    Vue.axios = _axios
    window.axios = _axios
    Object.defineProperties(Vue.prototype, {
      axios: {
        get() {
          return _axios
        }
      },
      $axios: {
        get() {
          return _axios
        }
      }
    })
  }
}
