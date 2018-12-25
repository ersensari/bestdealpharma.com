
const GlobalProperties = {
  install(Vue) {

    // Page Module Definition
    Object.defineProperty(Vue.prototype, '$Modules', {
      get() {
        return [
          { label: 'Content', id: 1 },
          { label: 'Products', id: 2 },
          { label: 'Contact Us', id: 3 },
          { label: 'Member Page', id: 4 },
          { label: 'Faq', id: 5 },
          { label: 'Sub Pages Content', id: 6 },
          { label: 'Sub Pages Linked', id: 7 }
        ]
      }
    })

  }
}

export default GlobalProperties
