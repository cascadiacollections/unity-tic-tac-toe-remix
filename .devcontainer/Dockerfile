FROM mcr.microsoft.com/devcontainers/universal:2

# Install additional dependencies needed for Unity development
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        mono-complete \
        git \
        curl \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Set a default working directory
WORKDIR /workspace
