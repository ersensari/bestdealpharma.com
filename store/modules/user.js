const baseApiUri = '/api/Authorization/'

const state = {
  authenticatedUser: null
}

const getters = {
  getAuthenticatedUserName: (state) => {
    if (state.authenticatedUser) {
      return state.authenticatedUser.name + ' ' + state.authenticatedUser.surname
    }
  }
}

const mutations = {
  setAuthenticatedUser: (state, user) => {
    state.authenticatedUser = user
  }
}
const actions = {
  getAuthenticatedUser: ({ commit }) => {
    window.axios.get(baseApiUri + 'GetAuthenticatedPerson')
      .then(response => {
        commit('setAuthenticatedUser', response.data)
      })
      .catch(err => {
        if () {
          err.response.status
        } 
        console.error(err)
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
