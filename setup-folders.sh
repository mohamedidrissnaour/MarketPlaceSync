#!/bin/bash

# ============================================================
#  MarketplaceSync — création des dossiers internes
#  À exécuter depuis la racine du projet MarketplaceSync/
# ============================================================

BASE="src/Services/MasterCatalog"

echo ">>> Création des dossiers internes MasterCatalog..."

# Domain
mkdir -p "$BASE/MasterCatalog.Domain/Entities"
mkdir -p "$BASE/MasterCatalog.Domain/Interfaces"
mkdir -p "$BASE/MasterCatalog.Domain/ValueObjects"

# Application
mkdir -p "$BASE/MasterCatalog.Application/Commands/CreateProduct"
mkdir -p "$BASE/MasterCatalog.Application/Commands/UpdateProduct"
mkdir -p "$BASE/MasterCatalog.Application/Queries/GetProduct"
mkdir -p "$BASE/MasterCatalog.Application/Queries/GetAllProducts"
mkdir -p "$BASE/MasterCatalog.Application/DTOs"
mkdir -p "$BASE/MasterCatalog.Application/Interfaces"

# Infrastructure
mkdir -p "$BASE/MasterCatalog.Infrastructure/Persistence"
mkdir -p "$BASE/MasterCatalog.Infrastructure/Repositories"
mkdir -p "$BASE/MasterCatalog.Infrastructure/Configuration"

# API
mkdir -p "$BASE/MasterCatalog.API/Controllers"
mkdir -p "$BASE/MasterCatalog.API/Middlewares"

# Shared Contracts
mkdir -p "src/Shared/Shared.Contracts/Events"
mkdir -p "src/Shared/Shared.Contracts/Models"

echo ""
echo ">>> Dossiers créés avec succès !"
echo ">>> Ouvre VS Code et vérifie l'arborescence dans l'explorateur."
