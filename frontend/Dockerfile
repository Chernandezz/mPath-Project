# Usa la imagen base de Node.js para Angular
FROM node:20 AS build
WORKDIR /app

# Copia los archivos del proyecto y instala dependencias
COPY package.json package-lock.json ./
RUN npm install

# Copia el resto de los archivos y construye la aplicación
COPY . .
RUN npm run build -- --configuration production

# Usa una imagen ligera de Nginx para servir la aplicación
FROM nginx:alpine
COPY --from=build /app/dist/frontend /usr/share/nginx/html
EXPOSE 80