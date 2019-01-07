export default [
  {
    name: 'APP_LOGOUT',
    callback: function (e) {
      this.axios.post('/account/logout').then(response => {
        window.localStorage.removeItem('user')
        window.localStorage.removeItem('token')
        this.$toastr('success', 'Logout successfully')
        this.$router.replace({path: '/login'})
      })
    }
  },
  {
    name: 'APP_PAGE_LOADED',
    callback: function (e) {
    }
  },
  {
    name: 'APP_AUTH_FAILED',
    callback: function (e) {
      this.$router.push('/login')
      this.$message.error('Token has expired')
    }
  },
  {
    name: 'APP_BAD_REQUEST',
    // @error api response data
    callback: function (msg) {
      this.$message.error(msg)
    }
  },
  {
    name: 'APP_ACCESS_DENIED',
    // @error api response data
    callback: function (msg) {
      this.$message.error(msg)
      this.$router.push('/forbidden')
    }
  },
  {
    name: 'APP_RESOURCE_DELETED',
    // @error api response data
    callback: function (msg) {
      this.$message.success(msg)
    }
  },
  {
    name: 'APP_RESOURCE_UPDATED',
    // @error api response data
    callback: function (msg) {
      this.$message.success(msg)
    }
  },
  {
    name: 'APP_SEARCH_DRUG',
    callback: function (searchText) {
      this.$router.push({name: 'product-search', params: {pSearchText: searchText}});
    }
  },
  {
    name: 'APP_ADD_TO_CART',
    callback: function (product) {
      window.getApp.basket = product
    }
  }
]
