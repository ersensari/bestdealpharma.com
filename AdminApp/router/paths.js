export default [

  {
    path: '*',
    meta: {
      public: true
    },
    redirect: {
      path: '/404'
    }
  },
  {
    path: '/404',
    meta: {
      public: true
    },
    name: 'NotFound',
    component: () => import(
      /* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
      `@/pages/NotFound.vue`
    )
  },
  {
    path: '/403',
    meta: {
      public: true
    },
    name: 'AccessDenied',
    component: () => import(
      /* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
      `@/pages/Deny.vue`
    )
  },
  {
    path: '/500',
    meta: {
      public: true
    },
    name: 'ServerError',
    component: () => import(
      /* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
      `@/pages/Error.vue`
    )
  },
  {
    path: '/login',
    meta: {
      public: true
    },
    name: 'Login',
    component: () => import(
      /* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
      `@/pages/Login.vue`
    )
  },
  {
    path: '/',
    meta: {},
    name: 'Root',
    redirect: {
      name: 'Dashboard'
    }
  },
  {
    path: '/dashboard',
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Dashboard',
    component: () => import(
      /* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
      `@/pages/Dashboard.vue`
    )
  }
]
