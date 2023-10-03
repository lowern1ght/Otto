import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import mkcert from 'vite-plugin-mkcert'

const listenPort = "LISTEN_PORT"
const listenPortSsl = "LISTEN_PORT_SSL"

export const defaultPort = 6000

const optionsSsl = {
  https: true,
  strictPort: true,
  port: import.meta[listenPortSsl]
}

const options = {
  https: true,
  strictPort: true,
  port: import.meta[listenPort] ?? defaultPort
}

export default defineConfig({
  plugins: [react(), mkcert()],
  server: options,
  preview: optionsSsl
})
