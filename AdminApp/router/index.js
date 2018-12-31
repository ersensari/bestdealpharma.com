import Vue from 'vue'
import Router from 'vue-router'
import paths from './paths'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

Vue.use(Router)
const router = new Router({
  base: '/',
  mode: 'hash',
  linkActiveClass: 'active',
  routes: paths
})

router.beforeEach((to, from, next) => {
  NProgress.start()
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!window.localStorage.getItem('token')) {
      next({
        path: '/login',
        params: { nextUrl: to.fullPath }
      })
    } else {
      let user = window.$myLocalStorage.getEnc('user')
      if (to.matched.some(record => record.meta.is_admin)) {
        if (user.roles && user.roles.filter(x => x.name == 'Admin').length > 0) {
          next()
        } else {
          next({ name: 'AccessDenied' })
        }
      } else {
        next()
      }
    }
  } else if (to.matched.some(record => record.meta.guest)) {
    if (!window.localStorage.getItem('token')) {
      next()
    } else {
      next({ name: '/' })
    }
  } else {
    next()
  }
})

router.afterEach((to, from) => {
  NProgress.done()
})

export default router
