import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  resolve: {
    alias: {
      '@components': path.resolve(__dirname, './src/components'),
      '@api': path.resolve(__dirname, './src/api'),
      '@type': path.resolve(__dirname, './src/types'),
      '@routes': path.resolve(__dirname, './src/routes'),
      '@assets': path.resolve(__dirname, './src/assets'),
      '@data': path.resolve(__dirname, './src/data'),
    },
  },
  plugins: [react()],
});
