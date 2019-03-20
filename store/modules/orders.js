/* eslint eqeqeq: "error" */

const apiPath = '/api/order/';

const state = {
  all: [],
  errors: [],
  prescriptions: []
};

const getters = {};

const mutations = {
  setItems(state, items) {
    state.all = items
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
  setPrescriptions(state, items) {
    state.prescriptions = items;
  }
};

// Actions
const actions = {
  getAll({commit}) {
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath + 'getOrders')
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
  getAllForAdmin({commit}, status) {
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath + 'getAllOrders?status=' + status)
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
  },

  getPrescriptions({commit}) {
    return new Promise((resolve, reject) => {
      window.axios
        .get(apiPath + 'getPrescription')
        .then(response => {
          commit('setPrescriptions', response.data);
          resolve(response)
        })
        .catch(e => {
          reject(e)
        })
    })
  },
  createOrder({commit}, orderModel) {
    return new Promise((resolve, reject) => {
      window.axios({
        method: 'post',
        url: apiPath + 'createOrder',
        data: orderModel
      })
        .then(response => {
          resolve(response)
        })
        .catch(e => {
          reject(e)
        })
    })
  },
  onCancel({commit}, id) {
    return new Promise((resolve, reject) => {
      window.axios({
        method: 'post',
        url: apiPath + 'cancelOrder/' + id
      }).then(
        response => {
          resolve(response)
        }
      ).catch(e => {
        reject(e)
      })
    })
  },
  onArchived({commit}, id) {
    return new Promise((resolve, reject) => {
      window.axios({
        method: 'post',
        url: apiPath + 'archiveOrder/' + id
      }).then(
        response => {
          this._vm.$toastr('info', 'The order has been moved to archive');
          resolve(response)
        }
      ).catch(e => {
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
