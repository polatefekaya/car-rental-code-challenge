FROM node:22-alpine AS builder
WORKDIR /app
COPY package.json package-lock.json ./

RUN npm ci && npm run builder
FROM nginx:1.23-alpine
COPY --from=builder /app/build /usr/share/nginx/html
EXPOSE 5000