const apiPath = 'api/links/'

const state = {
  all: [],
  allFiltered: [],
  allHierarchical: [],
  errors: [],
  selected: null,
  parentItem: null,
  isNew: false
}

const getters = {
  getParentItems(state) {
    if (state.all) {
      if (state.parentItem == null) {
        return state.all.filter(x => x.parentId == null)
      } else {
        return state.all.filter(x => x.parentId == state.parentItem.parentId)
      }
    }
  },
  getChildItems: (state) => (url) => {
    if (url && state.allHierarchical.length > 0) {
      var link = state.allHierarchical.find(x => x.url == url)
      if (link && link.children) {
        return link.children
      } else {
        return null
      }
    } else {
      return null
    }
  },
  getLinkByUrl: (state) => (url) => {
    if (url && state.allHierarchical) {
      var link = state.allHierarchical.find(x => x.url == url)
      if (link) {
        return link
      } else {
        return null
      }
    } else {
      return null
    }
  },
  getLinkHierarchy(state) {

    let items = []
    let finditem = function (_id) {
      let item = state.all.find(x => x.id == _id)
      if (item && item != null) {
        items.splice(0, 0, item)
        if (item.parentId != null) {
          finditem(item.parentId)
        }
      }
    }
    if (state.parentItem != null) {
      finditem(state.parentItem.id)
    }
    return items
  }
}

const mutations = {
  setAllItems(state, items) {
    state.all = items
  },
  setChildItems(state, item) {
    if (state.all) {
      state.selected = null
      state.isNew = false
      state.parentItem = item
      let parentId = item != null ? item.id : null
      state.allFiltered = state.all.filter(x => x.parentId == parentId)
    }
  },
  setParentItems(state) {
    state.selected = null
    state.isNew = false
    let parentId = state.parentItem != null ? state.parentItem.parentId : null
    state.allFiltered = state.all.filter(x => x.parentId == parentId)
    state.parentItem = parentId != null ? state.all.find(x => x.id == parentId) : null
  },
  updateItem(state, item) {
    state.parentItem = state.all.find(x => x.id == item.parentId)
    let parentId = state.parentItem != null ? state.parentItem.id : null
    state.allFiltered = state.all.filter(x => x.parentId == parentId)
    this._vm.$toastr('success', 'Update Process Completed')
    
  },
  addItem(state, item) {
    this._vm.$toastr('success', 'Save Process Completed')
    state.all.push(item)
    state.allFiltered.push(item)
    state.selected = item
    state.isNew = false
  },
  setErrors(state, errors) {
    state.errors = errors
    if (errors.length > 0)
      this._vm.$toastr('error', 'Save Process Incompleted')
  },
  setSelectedItem(state, item) {
    state.isNew = false
    state.selected = item
  },
  newItem(state, item) {
    state.isNew = true
    state.selected = item
    this._vm.$toastr('info', 'New Record Created')
  },
  removeItem(state, payload) {
    state.all.splice(state.all.findIndex(x => x.id == payload.id), 1)
    state.allFiltered = state.all.filter(x => x.parentId == payload.parentId)
    state.selected = null
    this._vm.$toastr('success', 'Delete Process Completed')
  },
  setLinkHierarchy(state, items) {
    state.allHierarchical = items
  }
}

// Actions
const actions = {
  getAll({ commit }, layout) {
    window.axios.get(apiPath, { params: { layout: layout } })
      .then(response => {
        commit('setAllItems', response.data)
        commit('setChildItems', null) // Set root items
      })
      .catch(e => {
        console.error(e)
      })
  },
  GetLinkHierarchy({ commit }, layout) {
    return new Promise((resolve, reject) => {
      window.axios.get(apiPath + 'GetLinkHierarchy', { params: { layout: layout } })
        .then(response => {
          commit('setLinkHierarchy', response.data)
          resolve(response.data)
        })
        .catch(e => {
          commit('setErrors', e)
          reject(e)
        })
    })
  },
  onSave({ commit }, payload) {
    return new Promise((resolve, reject) => {
      if (payload.id == 0) {
        window.axios({
          method: 'post',
          url: apiPath,
          data: payload
        })
          .then(response => {
            commit('addItem', response.data)
            resolve(response)
          })
          .catch(e => {
            reject(e)
          })
      } else {
        window.axios({
          method: 'put',
          url: apiPath + payload.id,
          data: payload
        })
          .then(response => {
            commit('updateItem', response.data)
            resolve(response)
          })
          .catch(e => {
            console.error(e)
            reject(e)
          })
      }
    })
  },
  onNew({ commit }) {
    return new Promise((resolve, reject) => {
      window.axios.get(apiPath + '-1')
        .then(response => {
          this._vm.$toastr('info', 'New Record Created')
          resolve(response)
        })
        .catch(e => {
          reject(e)
        })
    })
  },
  onDelete({ commit }, id) {
    return new Promise((resolve, reject) => {
      window.axios.delete(apiPath + id)
        .then(response => {
          commit('removeItem', response.data)
          resolve(response)
        })
        .catch(e => {
          this._vm.$toastr('error', 'Delete Process Incompleted')
          console.error(e)
          reject(e)
        })
    })
  },
  onItemSelected({ commit }, payload) {
    commit('setSelectedItem', payload)
  },
  getChildItems({ commit }, payload) {
    commit('setChildItems', payload)
  },
  getParentItems({ commit }) {
    commit('setParentItems')
  },
  getSingleByUrl({ commit }, url) {
    return new Promise((resolve, reject) => {
      window.axios.get(apiPath + 'GetByUrl?layout=0&url=' + url)
        .then(response => {
          resolve(response.data)
        })
        .catch(e => {
          reject(e)
        })
    })
  }

}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
