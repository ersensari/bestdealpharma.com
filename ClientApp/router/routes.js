import HomePage from 'components/HomePage'
import HowToOrder from 'components/HowToOrder'
import NewOrder from 'components/NewOrder'
import Faq from 'components/Faq'
import Policies from 'components/Policies'
import ContactUs from 'components/ContactUs'
import AboutUs from 'components/AboutUs'
import ShoppingCart from 'components/ShoppingCart'
import Register from 'components/Register'
import Account from 'components/Account'
import Information from 'components/AccountPartials/Information'
import MyOrders from 'components/AccountPartials/MyOrders'
import OrderRefill from 'components/AccountPartials/OrderRefill'
import Addresses from 'components/AccountPartials/Addresses'
import CheckOut from "components/CheckOut";
import Login from "components/Login";


export const routes = [
  {name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home', meta: {requiresAuth: false}},
  {
    name: 'how-to-order',
    path: '/how-to-order',
    component: HowToOrder,
    display: 'How To Order',
    meta: {requiresAuth: false}
  },
  {
    name: 'new-order',
    path: '/new-order',
    component: NewOrder,
    display: 'New Order',
    props: true,
    meta: {requiresAuth: false},
    children: [
      {
        name: 'product-search',
        path: '/product-search/:pSearchText?',
        component: NewOrder,
        display: 'Product Search',
        props: true,
        meta: {requiresAuth: false}
      }
    ]
  },
  {
    name: 'refill-order',
    path: '/order-refill',
    redirect: '/account/order-refill',
    display: 'Refill Order',
    meta: {requiresAuth: false}
  },
  {name: 'faq', path: '/faq', component: Faq, display: 'Faq', meta: {requiresAuth: false}},
  {name: 'policies', path: '/policies', component: Policies, display: 'Policies', meta: {requiresAuth: false}},
  {name: 'contact-us', path: '/contact-us', component: ContactUs, display: 'Contact Us', meta: {requiresAuth: false}},
  {name: 'about-us', path: '/about-us', component: AboutUs, display: 'About Us', meta: {requiresAuth: false}},
  {
    name: 'register',
    path: '/register',
    component: Register,
    display: 'Register',
    hideOnMenu: true,
    meta: {
      requiresAuth: false
    }
  },
  {
    name: 'login',
    path: '/login',
    component: Login,
    display: 'Login',
    hideOnMenu: true,
    meta: {
      requiresAuth: false
    }
  },
  {
    name: 'checkout',
    path: '/checkout',
    component: CheckOut,
    display: 'Check Out',
    hideOnMenu: true,
    meta: {requiresAuth: true}
  },
  {
    name: 'account',
    path: '/account',
    redirect: '/account/information',
    component: Account,
    display: 'Account',
    hideOnMenu: true,
    meta: {requiresAuth: true},
    children: [
      {
        name: 'account_information',
        path: '/account/information',
        component: Information,
        display: 'Information',
        meta: {requiresAuth: true}
      },
      {
        name: 'account_myorders',
        path: '/account/my-orders',
        component: MyOrders,
        display: 'My Orders',
        meta: {requiresAuth: true}
      },
      {
        name: 'account_orderrefill',
        path: '/account/order-refill',
        component: OrderRefill,
        display: 'Order Refill',
        meta: {requiresAuth: true}
      },
      {
        name: 'account_addresses',
        path: '/account/addresses',
        component: Addresses,
        display: 'Shipping Addresses',
        meta: {requiresAuth: true}
      },

    ]
  },
  {
    name: 'shopping-cart',
    path: '/shopping-cart',
    component: ShoppingCart,
    display: 'Shopping Cart',
    hideOnMenu: true,
    props: true,
    meta: {requiresAuth: false},
    children: [{
      name: 'add-to-shopping-cart',
      path: '/shopping-cart/add/:productId?',
      component: ShoppingCart,
      display: 'Add To Shopping Cart',
      props: true,
      meta: {requiresAuth: false}
    }]
  }
];
