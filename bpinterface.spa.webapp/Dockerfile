FROM node:18.9.0
# Create app directory
WORKDIR /app
# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY ["bpinterface.spa.webapp/package.json", "package.json"]
COPY ["bpinterface.spa.webapp/package-lock.json", "package-lock.json"]
RUN rm -rf node_modules
RUN npm install
#To bundle your app’s source code inside the Docker image, use the COPY instruction:
COPY ["bpinterface.spa.webapp/src/", "./src"]
COPY ["bpinterface.spa.webapp/public/", "./public"]
#Your app binds to port 3000 so you’ll use the EXPOSE instruction to have it mapped by the docker daemon:
EXPOSE 3000
CMD ["npm", "start"]