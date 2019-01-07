import HomePage from 'components/HomePage'
import HowToOrder from 'components/HowToOrder'
import NewOrder from 'components/NewOrder'
import RefillOrder from 'components/RefillOrder'
import Faq from 'components/Faq'
import Policies from 'components/Policies'
import ContactUs from 'components/ContactUs'
import AboutUs from 'components/AboutUs'
import ShoppingCart from 'components/ShoppingCart'

export const routes = [
  {name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home'},
  {name: 'how-to-order', path: '/how-to-order', component: HowToOrder, display: 'How To Order'},
  {
    name: 'new-order',
    path: '/new-order',
    component: NewOrder,
    display: 'New Order',
    props: true,
    children: [
      {
        name: 'product-search',
        path: '/product-search/:pSearchText?',
        component: NewOrder,
        display: 'Product Search',
        props: true
      }
    ]
  },
  {
    name: 'refill-order',
    path: '/refill-order',
    component: RefillOrder,
    display: 'Refill Order'
  },
  {name: 'faq', path: '/faq', component: Faq, display: 'Faq'},
  {name: 'policies', path: '/policies', component: Policies, display: 'Policies'},
  {name: 'contact-us', path: '/contact-us', component: ContactUs, display: 'Contact Us'},
  {name: 'about-us', path: '/about-us', component: AboutUs, display: 'About Us'},
  {
    name: 'shopping-cart',
    path: '/shopping-cart',
    component: ShoppingCart,
    display: 'Shopping Cart',
    hideOnMenu: true,
    props: true,
    children: [{
      name: 'add-to-shopping-cart',
      path: '/shopping-cart/add/:productId?',
      component: ShoppingCart,
      display: 'Add To Shopping Cart',
      props: true
    }]
  }
];
