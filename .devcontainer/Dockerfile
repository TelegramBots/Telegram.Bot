ARG DOTNET_VERSION=6.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-bullseye-slim
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=${USER_UID}
ENV DEBIAN_FRONTEND=noninteractive

# Install required and misc tools
RUN apt-get update && apt-get -y install --no-install-recommends apt-utils dialog 2>&1 \
    && apt-get -y install openssh-client less iproute2 apt-transport-https gnupg2 curl lsb-release \
    git procps ca-certificates vim nano groff zip file jq wget zsh \
    # Create a non-root user to use if preferred - see https://aka.ms/vscode-remote/containers/non-root-user.
    && groupadd --gid $USER_GID $USERNAME \
    && useradd -s /bin/bash --uid $USER_UID --gid $USER_GID -m $USERNAME \
    # [Optional] Add sudo support for the non-root user
    && apt-get install -y sudo \
    && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME\
    && chmod 0440 /etc/sudoers.d/$USERNAME

# Cleanup APT
RUN apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*

RUN chsh -s $(which zsh) ${USERNAME}
USER ${USERNAME}

RUN sh -c "$(curl -fsSL https://raw.github.com/ohmyzsh/ohmyzsh/master/tools/install.sh)"
# This is needed for Global Tools to work
ENV PATH="${PATH}:/home/${USERNAME}/.dotnet/tools"
# Install .NET Global Tools
RUN dotnet tool install -g dotnet-format \
    && dotnet tool install -g microsoft.dotnet-httprepl

ENV DEBIAN_FRONTEND=dialog

WORKDIR /workspace
