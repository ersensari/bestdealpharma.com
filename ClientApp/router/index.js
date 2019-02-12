import Vue from 'vue'
import VueRouter from 'vue-router'
import {routes} from './routes'
import NProgress from "nprogress";

Vue.use(VueRouter);

let router = new VueRouter({
  mode: 'history',
  routes
});

router.beforeEach((to, from, next) => {
    NProgress.start();
    if (to.matched.some(record => record.meta.requiresAuth)) {
      if (!window.localStorage.getItem('token')) {
        next({
          path: '/login',
          params: {returnUrl: to.fullPath}
        })
      } else {
        next()
      }
    } else {
      next()
    }
  }
)

router.afterEach((to, from) => {
  NProgress.done()
});


export default router
