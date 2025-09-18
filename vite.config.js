import { defineConfig } from "vite";

// https://vitejs.dev/config/
export default defineConfig({
  // GitHub Pages에서 저장소명이 '/resume-website/'이 되도록 설정
  base: "/resume-website/",
  build: {
    outDir: "dist",
    assetsDir: "assets",
  },
});
