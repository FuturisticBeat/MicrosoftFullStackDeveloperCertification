#!/bin/bash

# Variables
# API_URL="http://localhost:5083/swagger/v1/swagger.json"
# OUTPUT_DIR="../Frontend/GeneratedClients"
# CLASS_NAME="ProductsApiClient"
# NAMESPACE_NAME="ProductsApi"

# Generate client using NSwag openapi2csclient command
# nswag openapi2csclient /input:$API_URL /output:$OUTPUT_DIR/$CLASS_NAME.cs /classname:$CLASS_NAME /namespace:$NAMESPACE_NAME

# Generate client using NSwag and json configuration
nswag run nswag.json