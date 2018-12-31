const Menu = [
  {
    title: 'Dashboard',
    icon: 'dashboard',
    name: 'Dashboard'
  },
  { divider: true },
  {
    title: 'Membership',
    icon: 'streetview',
    name: 'Membership',
    items: [
      { name: 'Members', title: 'Members', component: 'Members' },
      { name: 'Orders', title: 'Orders', component: 'Orders' },
      { name: 'Messages', title: 'Messages', component: 'Messages' }
    ]
  },
  { divider: true },
  {
    title: 'Management',
    icon: 'settings',
    name: 'Pages',
    items: [
      { name: 'Links', title: 'Links', component: 'Links' },
      { name: 'Pages', title: 'Pages', component: 'Pages' },
      { name: 'Products', title: 'Products', component: 'Products' },
      { name: 'Settings', title: 'Settings', component: 'Settings' }
    ]
  },
  { divider: true },
  {
    title: 'Reports',
    icon: 'list',
    items: [
      { name: 'OrderList', title: 'OrderList', component: 'OrderList' }
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
