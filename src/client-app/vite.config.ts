import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import mkcert from 'vite-plugin-mkcert'

const listenPort = "LISTEN_PORT"

export default defineConfig({
  plugins: [react(), mkcert()],
  
  server: {
    port: import.meta[listenPort] ?? 6901,
    strictPort: false,
    cors: false,
  },
  
  preview: {
    cors: false,
    strictPort: true,
    port: import.meta[listenPort] ?? 6901,
  }
})