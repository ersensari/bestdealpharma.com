import Addresses from "components/AccountPartials/Addresses";

const baseApiUri = '/api/Authorization/';

const state = {
  authenticatedUser: null,
  isAuthenticated: false,
  addresses: []
};

const getters = {
  getAuthenticatedUserName: state => {
    if (state.authenticatedUser) {
      return state.authenticatedUser.person.name + ' ' + state.authenticatedUser.person.surname
    }
  },
  isAuthenticated: state => {
    return state.isAuthenticated
  },
  authenticatedUser: state => {
    return state.authenticatedUser;
  },
  userAddresses: state => {
    return state.addresses;
  }
};

const mutations = {
  setAuthenticatedUser: (state, user) => {
    state.authenticatedUser = user
  },
  isAuthenticated: (state, status) => {
    state.isAuthenticated = status
  },
  setAddresses: (state, address) => {
    state.addresses = address;
  },
  updateAddresses: (state, address) => {
    const i = state.addresses.findIndex(x => x.id === address.id);
    state.addresses[i] = address;
  }

};
const actions = {
  getAuthenticatedUser: ({commit}) => {
    if (window.localStorage.getItem('token')) {
      window.axios
        .get(baseApiUri + 'GetAuthenticatedUser')
        .then(response => {
          commit('setAuthenticatedUser', response.data);
          commit('isAuthenticated', response.data != null);

        })
        .catch(err => {
          if (err.response.status === 401) {
            window.localStorage.removeItem('token');
            window.localStorage.removeItem('user');
            commit('isAuthenticated', false);
          } else {
            console.error(err)
          }
        })
    } else {
      commit('setAuthenticatedUser', null);
      commit('isAuthenticated', false);
    }
  },

  getPersonAddresses({commit}, personId) {
    return new Promise((resolve, reject) => {
      window.axios.get('/api/addresses/GetPersonAddresses/' + personId)
        .then(response => {
          commit('setAddresses', response.data);
          resolve(response)
        }).catch(e => {
        reject(e);
      })
    })
  },

  saveAddress({commit}, payload) {
    return new Promise((resolve, reject) => {
      if (payload.id === 0) {
        window
          .axios({
            method: 'post',
            url: 'api/addresses',
            data: payload
          })
          .then(response => {
            commit('setAddresses', response.data);
            resolve(response)
          })
          .catch(e => {
            reject(e)
          })
      } else {
        window
          .axios({
            method: 'put',
            url: 'api/addresses/' + payload.id,
            data: payload
          })
          .then(response => {
            commit('updateAddresses', response.data);
            resolve(response)
          })
          .catch(e => {
            reject(e)
          })
      }
    })
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
