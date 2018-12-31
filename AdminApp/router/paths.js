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
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/NotFound.vue`)
  },
  {
    path: '/403',
    meta: {
      public: true
    },
    name: 'AccessDenied',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Deny.vue`)
  },
  {
    path: '/500',
    meta: {
      public: true
    },
    name: 'ServerError',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Error.vue`)
  },
  {
    path: '/login',
    meta: {
      public: true
    },
    name: 'Login',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Login.vue`)
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
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Dashboard.vue`)
  },
  {
    path: '/links',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Links.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Links'
  },
  {
    path: '/pages',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Pages.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Pages'
  },
  {
    path: '/settings',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Settings.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Settings'
  },
  {
    path: '/products',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Products.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Products'
  },
  {
    path: '/members',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Members.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Members'
  },
  {
    path: '/orders',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Orders.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Orders'
  },
  {
    path: '/messages',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/Messages.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'Messages'
  },
  {
    path: '/order-list',
    component: () =>
      import(/* webpackChunkName: "routes" */
      /*  webpackMode: "eager", */
        `@/pages/OrderList.vue`),
    meta: {
      breadcrumb: true,
      requiresAuth: true,
      is_admin: true
    },
    name: 'OrderList'
  }
]
