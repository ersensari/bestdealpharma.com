const Menu = [
  {
    title: 'Dashboard',
    group: 'apps',
    icon: 'dashboard',
    name: 'Dashboard'
  },
  { divider: true },
  {
    title: 'Pages',
    group: 'widgets',
    icon: 'widgets',
    name: 'Pages',
    items: [
      { name: 'Links', title: 'Links', component: 'components/social' },
      { name: 'statistic', title: 'Statistic', badge: 'new', component: 'components/statistic' },
      { name: 'chart', title: 'Chart', component: 'components/chart' },
      { name: 'list', title: 'List', component: 'components/widget-list' }
    ]
  },
  { divider: true },
  {
    title: 'Pages',
    group: 'extra',
    icon: 'list',
    items: [
      { name: 'Login', title: 'Login', component: 'Login' },
      { name: '404', title: '404', component: 'NotFound' },
      { name: '403', title: '403', component: 'AccessDenied' },
      { name: '500', title: '500', component: 'ServerError' }
    ]
  }
]
// reorder menu
Menu.forEach((item) => {
  if (item.items) {
    item.items.sort((x, y) => {
      let textA = x.title.toUpperCase()
      let textB = y.title.toUpperCase()
      return (textA < textB) ? -1 : (textA > textB) ? 1 : 0
    })
  }
})

export default Menu
