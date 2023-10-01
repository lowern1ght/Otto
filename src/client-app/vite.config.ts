import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import mkcert from 'vite-plugin-mkcert'

function option(port: number) {
  return  {
    https: true,
    strictPort: true,
    port: port
  }
}

export default defineConfig({
  plugins: [react(), mkcert()],
  server: option(9000),
  preview: option(80)
})
