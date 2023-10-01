import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import {devPort, port} from "./src/vite-env";

function option(port: number) {
  return  {
    https: true,
    strictPort: true,
    port: port
  }
}

export default defineConfig({
  plugins: [react()],
  server: option(devPort),
  preview: option(port)
})
