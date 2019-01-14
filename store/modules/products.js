const apiPath = '/api/products/';

const state = {
  all: [],
  errors: [],
  searchResult: [],
  searchCriteria: {
    keyword: '',
    byLetter: false
  }
};

const getters = {};

const mutations = {
  setItems(state, items) {
    state.all = items
  },
  setSearchResult(state, items) {
    state.searchResult = items;
  },
  updateItem(state, item) {
    this._vm.$toastr('success', 'Update Process Completed')
  },
  addItem(state, item) {
    this._vm.$toastr('success', 'Save Process Completed');
    state.all.push(item)
  },
  removeItem(state, payload) {
    state.all.splice(state.all.findIndex(x => x.id === payload.id), 1);
    this._vm.$toastr('success', 'Delete Process Completed')
  },
  setErrors(state, error) {
    state.errors.push(error);
    if (state.errors.length > 0) {
      this._vm.$toastr('error', 'Unexpected error occurred while processing');
      console.error(error)
    }
  },
  setSearchCriteria(state, criteria) {
    state.searchCriteria = criteria;
  }
};

// Actions
const actions = {
  getAll({commit}) {
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath)
        .then(response => {
          commit('setItems', response.data);
          resolve(response)
        })
        .catch(e => {
          commit('setErrors', e);
          reject(e)
        })
    })
  },
  find({commit}, criteria) {
    commit('setSearchCriteria', criteria);
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath + 'find', {
            params:
              {
                keyword: criteria.keyword,
                byLetter: criteria.byLetter
              }
          }
        )
        .then(response => {
          commit('setSearchResult', response.data);
          resolve(response)
        })
        .catch(e => {
          commit('setErrors', e);
          reject(e)
        })
    })
  },

  onSave({commit}, payload) {
    return new Promise((resolve, reject) => {
      if (payload.id === 0) {
        window
          .axios({
            method: 'post',
            url: apiPath,
            data: payload
          })
          .then(response => {
            commit('addItem', response.data);
            resolve(response)
          })
          .catch(e => {
            commit('setErrors', e);
            reject(e)
          })
      } else {
        window
          .axios({
            method: 'put',
            url: apiPath + payload.id,
            data: payload
          })
          .then(response => {
            commit('updateItem', response.data);
            resolve(response)
          })
          .catch(e => {
            commit('setErrors', e);
            reject(e)
          })
      }
    })
  },

  onNew({commit}) {
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath + '-1')
        .then(response => {
          this._vm.$toastr('info', 'New Record Created');
          resolve(response)
        })
        .catch(e => {
          commit('setErrors', e);
          reject(e)
        })
    })
  },

  onDelete({commit}, id) {
    return new Promise((resolve, reject) => {
      window.axios
        .delete(apiPath + id)
        .then(response => {
          commit('removeItem', response.data);
          resolve(response)
        })
        .catch(e => {
          commit('setErrors', e);
          reject(e)
        })
    })
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
