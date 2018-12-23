const state =
{
  data: {
    show: false,
    text: '',
    color: ''
  }
}

const mutations = {
  setData(state, data) {
    state.data = data
  }
}

const actions = {
  show({ commit }, data) {
    commit('setData', data)
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
