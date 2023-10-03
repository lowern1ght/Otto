FROM node:lts-alpine

ENV NODE_ENV=production

WORKDIR /usr/src/app

COPY ["package.json", "package-lock.json*", "npm-shrinkwrap.json*", "./"]

RUN npm install --production --save  \
    && npm i -g vite ts

COPY . .
RUN npm run build

EXPOSE 80
EXPOSE 443

CMD ["npm", "run", "preview"]
