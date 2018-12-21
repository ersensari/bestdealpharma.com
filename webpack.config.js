const path = require('path')
const webpack = require('webpack')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const OptimizeCSSPlugin = require('optimize-css-assets-webpack-plugin')
const bundleOutputDir = './wwwroot/dist'
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = () => {
  console.log('Building for \x1b[33m%s\x1b[0m', process.env.NODE_ENV)

  const isDevBuild = !(process.env.NODE_ENV && process.env.NODE_ENV === 'production')
  const extractCSS = new ExtractTextPlugin('site.css')

  return [{
    stats: { modules: false },
    entry: {
      'main': './ClientApp/boot-app.js',
      'admin': './AdminApp/boot-app.js'
    },
    resolve: {
      extensions: ['.js', '.vue'],
      alias: isDevBuild ? {
        'vue$': 'vue/dist/vue',
        '@': path.resolve(__dirname, './AdminApp'),
        'components': path.resolve(__dirname, './ClientApp/components'),
        'views': path.resolve(__dirname, './ClientApp/views'),
        'utils': path.resolve(__dirname, './ClientApp/utils'),
        'api': path.resolve(__dirname, './ClientApp/store/api'),
        'plugins': path.resolve(__dirname, './plugins'),
        '@store': path.resolve(__dirname, './store')
      } : {
          '@': path.resolve(__dirname, './AdminApp'),
          'components': path.resolve(__dirname, './ClientApp/components'),
          'views': path.resolve(__dirname, './ClientApp/views'),
          'utils': path.resolve(__dirname, './ClientApp/utils'),
          'api': path.resolve(__dirname, './ClientApp/store/api'),
          'plugins': path.resolve(__dirname, './plugins'),
          '@store': path.resolve(__dirname, './store')
        }
    },
    output: {
      path: path.join(__dirname, bundleOutputDir),
      filename: '[name].js',
      publicPath: '/dist/'
    },
    module: {
      rules: [
        { test: /\.vue$/, use: 'vue-loader' },
        { test: /\.js$/, use: 'babel-loader', exclude: /node_modules/ },
        { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader' }) },
        { test: /\.styl(us)?$/, use: ['vue-style-loader', 'css-loader', 'stylus-loader'] },
        { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=100000' },
        { test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/, use: 'url-loader?limit=100000' }
      ]
    },
    plugins: [
      new VueLoaderPlugin(),
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require('./wwwroot/dist/vendor-manifest.json')
      })
    ].concat(isDevBuild ? [
      // Plugins that apply in development builds only
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map', // Remove this line if you prefer inline source maps
        moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
      })
    ] : [
        // Plugins that apply in production builds only
        new webpack.optimize.UglifyJsPlugin(),
        extractCSS,
        // Compress extracted CSS.
        new OptimizeCSSPlugin({
          cssProcessorOptions: {
            safe: true
          }
        })
      ])
  }]
}
