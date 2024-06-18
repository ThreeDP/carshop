#!/bin/sh

source /etc/os-release

# # Download Microsoft signing key and repository
wget https://packages.microsoft.com/config/$ID/$VERSION_ID/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

# # Install Microsoft signing key and repository
dpkg -i packages-microsoft-prod.deb

# # Clean up
rm packages-microsoft-prod.deb

# # Update packages
apt-get update