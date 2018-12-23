
const apiPath = 'api/pages/'

const state = {
  all: [],
  errors: [],
  selected: null,
  isNew: false
}

const getters = {}

const mutations = {
  setItems(state, items) {
    state.all = items
  },
  updateItem(state, item) {
    this._vm.$toastr('success', 'Kaydetme işlemi başarılı.')
  },
  addItem(state, item) {
    this._vm.$toastr('success', 'Kaydetme işlemi başarılı.')
    state.all.push(item)
    state.selected = item
    state.isNew = false
  },
  setErrors(state, errors) {
    state.errors = errors
    if (errors.length > 0)
      this._vm.$toastr('error', errors.map(e => {
        return e.message
      }).join('<br/>'))
  },
  setSelectedItem(state, item) {
    state.isNew = false
    state.selected = item
  },
  newItem(state, item) {
    state.isNew = true
    item.htmlContent = ''
    state.selected = item
    this._vm.$toastr('info', 'Yeni Kayıt.')
  },
  removeItem(state, payload) {
    state.all.splice(state.all.findIndex(x => x.id == payload.id), 1)
    state.selected = null
    this._vm.$toastr('success', 'Silme işlemi başarılı')
  }
}

// Actions
const actions = {
  getAll({ commit }) {
    commit('setErrors', [])

    window.axios.get(apiPath)
      .then(response => {
        commit('setItems', response.data)
      })
      .catch(e => {
        commit('setErrors', e.response.data)
      })
  },
  onSave({ commit }, payload) {
    commit('setErrors', [])

    if (payload.id == 0) {
      window.axios({
        method: 'post',
        url: apiPath,
        data: payload
      })
        .then(response => {
          commit('addItem', response.data)
        })
        .catch(e => {
          commit('setErrors', e.response.data)
        })
    } else {
      window.axios({
        method: 'put',
        url: apiPath + payload.id,
        data: payload
      })
        .then(response => {
          commit('updateItem', response.data)
        })
        .catch(e => {
          commit('setErrors', e.response.data)
        })
    }

  },
  onNew({ commit }) {
    window.axios.get(apiPath + '-1')
      .then(response => {
        commit('newItem', response.data)
      })
      .catch(e => {
        commit('setErrors', e.response.data)
      })

  },
  onDelete({ commit }, id) {
    window.axios.delete(apiPath + id)
      .then(response => {
        commit('removeItem', response.data)
      })
      .catch(e => {
        commit('setErrors', e.response.data)
      })
  },
  onCardSelected({ commit }, payload) {
    commit('setSelectedItem', payload)
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
