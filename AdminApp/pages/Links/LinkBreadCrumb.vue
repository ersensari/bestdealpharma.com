<template>
  <v-layout row>
    <v-breadcrumbs :items="links" divider="\">
      <template slot="item" slot-scope="props">
        <a small flat @click.prevent="getChildItems(props.item.link)">{{ props.item.text }}</a>
      </template>
    </v-breadcrumbs>
  </v-layout>
</template>
<script>
  export default {
    props: {
      items: { type: Array }
    },
    data() {
      return {
        links: []
      }
    },
    created() {
      this.getItemsMapped()
    },
    watch: {
      items() {
        this.getItemsMapped()
      }
    },
    methods: {
      getItemsMapped() {
        var _items = [{
          text: 'ROOT',
          disabled: false,
          href: '/admin#/links',
          link: null
        }]

       _items = _items.concat(this.items.map(x => {
          return {
            text: x.name,
            disabled: false,
            href: '/admin#/links/' + x.id,
            link: x
          }
        }))

        this.links = _items
      },
      getChildItems(item) {
        this.$emit("getChildItems", item);
      }
    }
  }
</script>
